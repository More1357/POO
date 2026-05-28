using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomatico
{
    public class CajaDeAhorro : CuentaBancaria 
    {
        public CajaDeAhorro(string nroCuenta) : base(nroCuenta){}
        public override bool Extraer(decimal monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine($"Error: El monto debe ser positivo.");
                return false;
            }

            else
            {
                decimal saldoActual = ObtenerSaldo();
                if(monto > saldoActual)
                {
                    Console.WriteLine($"Error: Saldo insuficiente.");
                    return false;
                }

                EstablecerSaldo(saldoActual - monto);
                Console.WriteLine($"Extracción exitossa: {monto:C}");
                return true;
            }
        }
    }
}
