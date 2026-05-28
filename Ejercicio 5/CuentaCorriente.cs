using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomatico
{
    public class CuentaCorriente : CuentaBancaria
    {
        private decimal descubierto;

        public CuentaCorriente(string nroCuenta, decimal descubierto) : base(nroCuenta)
        {
            this.descubierto = descubierto;
        }

        public override bool Extraer(decimal monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine($"Error: El monto a extraer debe ser positivo. Monto ingresado: {monto:C}");
                return false;
            }

            else
            {
                decimal saldoActual = ObtenerSaldo();
                decimal saldoPermitido = -descubierto;

                if (saldoActual - monto < saldoPermitido)
                {
                    Console.WriteLine($"Error: Excede el límite de descubierto. Límite: {descubierto:C}, Saldo actual: {saldoActual:C}, Monto solicitado: {monto:C}");
                    return false;
                }

                EstablecerSaldo(saldoActual - monto);
                Console.WriteLine($"Extracción exitosa: {monto:C}");
                return true;
            }

        }
    }
}


