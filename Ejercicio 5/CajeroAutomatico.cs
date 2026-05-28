using System;

namespace CajeroAutomatico
{
    public abstract class CuentaBancaria
    {
        private decimal saldo;
        private string nroCuenta;

        public CuentaBancaria (string nroCuenta)
        {
            this.nroCuenta = nroCuenta;
            this.saldo = 0;
        }

        public bool Depositar(decimal monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine($"Error: el monto debe ser mayor a cero.");
                return false;
            }

            else
            {
                saldo += monto;
                Console.WriteLine($"Deposito hecho: {monto:C}");
                return true;
            }
        }

        public abstract bool Extraer(decimal monto);

        public decimal MostrarSaldo()
        {
            Console.WriteLine($"Cuenta: {nroCuenta}; Saldo: {saldo:C}");
            return saldo;
        }

        protected decimal ObtenerSaldo()
        {
            return saldo;
        }

        protected void EstablecerSaldo(decimal nuevoSaldo)
        {
            saldo = nuevoSaldo;
        }
    } 
}

