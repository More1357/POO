using Clinicamedica;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMedica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new ClinicaContext();

            //seleccionar y cargar todos los turnos y sus relaciones
            var turnos = context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Medico)
                .Include(t => t.Especialidad)
                .Include(t => t.Estado)
                .OrderBy(t => t.Fecha)
                .ThenBy(t => t.Hora)
                .ToList();

            foreach (var t in turnos)
            {
                Console.WriteLine($"{t.Fecha} {t.Hora} | {t.Paciente.Nombre} {t.Paciente.Apellido} | {t.Medico.Nombre} {t.Medico.Apellido} | {t.Especialidad.Nombre} | {t.Estado.Descripcion}");
            }

            var turnos2 = context.Turnos.Where(t => t.Estado.Descripcion == "cancelado");
            foreach (var turno in turnos2)
            {

                Console.WriteLine($"{turno.Estado.Descripcion} | {turno.Especialidad.Nombre} | {turno.Paciente.Nombre}");
            }

            Paciente paciente = context.Pacientes.FirstOrDefault(p => p.Dni == 27999000);
            Console.WriteLine($"{paciente.Nombre} {paciente.Apellido}");

            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine($"MENÚ PRINCIPAL");
                Console.WriteLine("0. Salir");
                Console.WriteLine($"1. Dar de alta");
                Console.WriteLine($"2. Listar pacientes");
                Console.WriteLine("3. Asignar nueva intervención");
                Console.WriteLine("4. Costo total por intervenciones");
                Console.WriteLine("5. Reporte de las liquidaciones pendientes de pago de los pacientes");
                Console.Write("Seleccione una opción: ");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AltaPaciente(context);
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;

                    case 2:
                        ListarPacientes(context);
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;

                    case 3:
                        AsignarIntervencion(context);
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;

                    case 4:
                        CalcularCostoIntervenciones(context);
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;

                    case 5:
                        ReporteLiquidacionesPendientes(context);
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 0);
        }

        static void AltaPaciente(ClinicaContext clinica)
        {
            Console.WriteLine("DAR ALTA A PACIENTE");

            Console.WriteLine("DNI: ");
            int dni = int.Parse(Console.ReadLine());

            var existe = clinica.Pacientes.FirstOrDefault(p => p.Dni == dni);
            if (existe != null)
            {
                Console.WriteLine($"Error: Ya existe un paciente con DNI {dni}");
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Fecha de nacimiento (yyyy-MM-dd): ");
            string fechaNacimiento = Console.ReadLine();

            Console.Write("¿Tiene obra social? (S/N): ");
            string tieneObraSocial = Console.ReadLine().ToUpper();

            string? obraSocial = null;
            decimal? cobertura = null;

            if (tieneObraSocial == "S")
            {
                Console.Write("Nombre de la obra social: ");
                obraSocial = Console.ReadLine();

                Console.Write("Porcentaje de cobertura (0-100): ");
                cobertura = decimal.Parse(Console.ReadLine());
            }

            Paciente nuevoPaciente = new Paciente
            {
                Dni = dni,
                Nombre = nombre,
                Apellido = apellido,
                Telefono = telefono,
                Email = email,
                FechaNacimiento = fechaNacimiento,
                ObraSocial = obraSocial,
                CoberturaPorcentaje = cobertura
            };

            clinica.Pacientes.Add(nuevoPaciente);
            clinica.SaveChanges();

            Console.WriteLine($"Paciente {nombre} {apellido} dado de alta correctamente");
        }

        static void ListarPacientes(ClinicaContext clinica)
        {
            Console.WriteLine("LISTA DE PACIENTES");

            var pacientes = clinica.Pacientes.ToList();

            if (pacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
            }

            foreach (var p in pacientes)
            {
                Console.WriteLine($"{p.Dni} | {p.Nombre} {p.Apellido} | Telefono: {p.Telefono} | Email: {p.Email} | Fecha de Nacimiento: {p.FechaNacimiento}");
            }
        }

        static void AsignarIntervencion(ClinicaContext clinica)
        {
            Console.WriteLine("ASIGNAR INTERVENCIÓN A PACIENTE");

            Console.Write("DNI del paciente: ");
            int dni = int.Parse(Console.ReadLine());

            var paciente = clinica.Pacientes.FirstOrDefault(p => p.Dni == dni);
            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
            }

            Console.WriteLine($"Paciente: {paciente.Nombre} {paciente.Apellido}");

            Console.WriteLine("ID de especialidad: ");
            int idEspecialidad = int.Parse(Console.ReadLine());

            var especialidad = clinica.Especialidades.FirstOrDefault(equals => equals.IdEspecialidad == idEspecialidad);
            if (especialidad == null)
            {
                Console.WriteLine("Especialidad no encontrada");
            }

            Console.Write("Matrícula del médico: ");
            int matricula = int.Parse(Console.ReadLine());

            var medico = clinica.Medicos.FirstOrDefault(m => m.Matricula == matricula);
            if (medico == null)
            {
                Console.WriteLine("Medico no encontrado");
            }

            Random random = new Random();

            DateTime hoy = DateTime.Now;
            DateTime fechaMax = hoy.AddDays(90);
            int diasFuturo = random.Next(1, 90);
            DateTime fecha = hoy.AddDays(diasFuturo);

            int hora = random.Next(8, 18);
            DateTime fechaYhora = new DateTime(
                fecha.Year,
                fecha.Month,
                fecha.Day,
                hora,
                0,
                0
                );

            string fechaString = fechaYhora.ToString("dd-MM-YYYY");
            string horaString = fechaYhora.ToString("HH: mm");

            Console.WriteLine($"Fecha asignada: {fechaString}");
            Console.WriteLine($"Hora asignada: {horaString}");

            Turno nuevoTurno = new Turno
            {
                Dni = paciente.Dni,
                Matricula = medico.Matricula,
                IdEspecialidad = especialidad.IdEspecialidad,
                Fecha = fechaString,
                Hora = horaString,
                IdEstado = 1,
                Observaciones = "Intervención quirúrgica"
            };

            clinica.Turnos.Add(nuevoTurno);
            clinica.SaveChanges();

            Console.WriteLine($"Intervencion asignada para el {fechaString} a ñas {horaString}");

        }

        static void CalcularCostoIntervenciones(ClinicaContext clinica)
        {
            Console.WriteLine("CALCULAR COSTO DE INTERVENCIONES");

            Console.Write("Ingrese el DNI del paciente: ");
            int dni = int.Parse(Console.ReadLine());

            var paciente = clinica.Pacientes.FirstOrDefault(p => p.Dni == dni);
            if (paciente == null)
            {
                Console.WriteLine("Paciete no encontrado");
            }

            Console.WriteLine($"Paciente: {paciente.Nombre} {paciente.Apellido}\n");

            var turnosPaciente = clinica.Turnos
                .Where(t => t.Dni == paciente.Dni)
                .Include(t => t.Especialidad)
                .Include(t => t.Medico)
                .Include(t => t.Estado)
                .ToList();

            if (turnosPaciente.Count == 0)
            {
                Console.WriteLine("El paciente no tiene intervenciones registradas");
            }

            decimal costoTotal = 0;
            int contador = 1;

            foreach (var turno in turnosPaciente)
            {
                decimal costoIntervencion = turno.Especialidad.CostoBase;

                costoTotal += costoIntervencion;
                contador++;
            }

            Console.WriteLine($"Cantidad de intervenciones: {turnosPaciente.Count}");
            Console.WriteLine($"TOTAL a pagar: ${costoTotal}");
        }

        static void ReporteLiquidacionesPendientes(ClinicaContext clinica)
        {
            Console.WriteLine("REPORTE DE LIQUIDACIONES PENDIENTES");

            var turnosPendientes = clinica.Turnos
                .Where(t => t.IdEstado == 1)
                .Include(t => t.Paciente)
                .Include(t => t.Medico)
                .Include(t => t.Especialidad)
                .ToList();

            if (turnosPendientes.Count == 0)
            {
                Console.WriteLine("No hay liquidaciones pendientes de pago");
            }

            decimal totalGeneral = 0;

            foreach (var turno in turnosPendientes)
            {
                string identificador = $"{turno.Dni}-{turno.Matricula}-{turno.IdEspecialidad}";
                string pacienteNombre = $"{turno.Paciente.Nombre} {turno.Paciente.Apellido}";
                string medicoNombre = $"{turno.Medico.Nombre} {turno.Medico.Apellido} ({turno.Medico.Matricula})";

                string obraSocial = turno.Paciente.ObraSocial ?? "-";

                decimal importeBase = turno.Especialidad.CostoBase;
                decimal importeFinal = importeBase;

                if (turno.Paciente.CoberturaPorcentaje.HasValue)
                {
                    decimal descuento = importeBase * (turno.Paciente.CoberturaPorcentaje.Value / 100);
                    importeFinal = importeBase - descuento;
                }

                totalGeneral += importeFinal;
            }

            Console.WriteLine($"TOTAL GENERAL PENDIENTE: ${totalGeneral}");
        }
    }
}
