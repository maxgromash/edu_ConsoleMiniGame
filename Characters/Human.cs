using System;

namespace Characters
{
    public class Human
    {
        protected static Random rand = new Random();
        protected bool poisoned = false;
        protected double healthy;

        public double Health
        {
            get
            {
                return healthy;
            }
            set
            {
                if (value < 0)
                    healthy = 0;
                else
                    healthy = value;
            }

        }
        public void IsPoisoned()
        {
            Console.WriteLine("Он меня отравил!");
            poisoned = true;
        }
        public virtual double GetDamage(int damage)
        {
            return 0;
        }
        public virtual void DownGuard() { }

        /// <summary>
        /// Метод для проверки жив персонаж или нет
        /// </summary>
        /// <returns>Да или нет </returns>
       
        public bool IsAlive()
        {
            if (healthy > 0) return true;
            else return false;
        }
    }
}