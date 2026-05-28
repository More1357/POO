using System;

namespace CajeroAutomatico
{
    public class Banco
    {
        private CuentaBancaria[] cuentas;
        private int nroCuentas;
        private string nombre;

        public Banco()
        {
            cuentas = new CuentaBancaria[10];
            nroCuentas = 0;
        }

        public void AgregarCuenta(CuentaBancaria cuenta)
        {
            if (nroCuentas >= 10)
            {
                Console.WrtieLine("Se superó el límite de cuentas");
            }

            CuentaBancaria[nroCuentas] = cuenta;
            nroCuentas++;
        }

        private bool Transferir(CuentaBancaria origen, CuentaBancaria destino, decimal monto)
        {
            if(monto <= 0)
            {
                Console.WriteLine($"Error: El saldo debe ser positivo.");
                return false;
            }

            if (-(origen.Extraer(monto)))
            {
                return false;
            }

            for(int i = 0; i < 10; i++)
            {
                if (CuentaBancaria[i].nroCuenta == origen.nroCuenta)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if (CuentaBancaria[j].nroCuenta == destino.nroCuenta)
                        {
                            origen.saldo -= monto;
                            destino.saldo += monto;
                            return true;
                        }
                    }
                }
            }
        }
    }
}