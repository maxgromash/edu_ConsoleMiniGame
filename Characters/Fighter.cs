using System;

namespace Characters
{ 
    public class Fighter : Human
    {
        protected int damage;
        protected int guard;
        protected double evade;
        protected double totalDamage;

        public double TotalDamage
        {
            get
            {
                return totalDamage;
            }
            set
            {
                totalDamage = value;
            }
        }

        public double Damage
        {
            get
            {
                return damage;
            }
        }
        public override void DownGuard()
        {
            guard--;
        }
        public int Guard
        {
            get
            {
                return guard;
            }
        }

        public double Evade
        {
            get
            {
                return evade;
            }
        }

        public Fighter(int health, int damage, int guard, double evade)
        {
            this.damage = damage;
            this.guard = guard;
            this.evade = evade;
            Health = health;
            poisoned = false;
        }

        public override double GetDamage(int damage)
        {
            double total;
            if (damage - guard > Health) total = Health;
            else total = damage - guard;
            Health = Health - damage + guard;
            return total;
        }
        
        /// <summary>
        /// Метод для атаки
        /// </summary>
        /// <param name="enemy">Противник</param>
        virtual public void Attack(Human enemy)
        {
            TotalDamage = 0.0; //Итоговый урон
            var enm = (Fighter)enemy;

            if (poisoned) // Проверка отравлен персонаж или нет
            {
                if (healthy * 0.93 >= 5.0) healthy = healthy * 0.93;
                else healthy -= 5.0;
            }
            if (healthy > 0)
            {
                if (rand.NextDouble() >= enm.Evade) // Проверка увернулся противник или нет
                {
                    TotalDamage = enm.GetDamage(damage);
                    if (enm.Guard > 0) enm.DownGuard();
                }
                else
                {
                    Console.WriteLine("Увернулся, подлец!");
                }

                if (enemy is Samurai && enemy.Health > 0 && Health > 0)
                {
                    Samurai emnS = (Samurai)enm;
                    if (rand.NextDouble() < emnS.Retaliation)
                    {
                        Console.WriteLine("Самурай наносит ответный удар");
                        emnS.Attack(this);
                    }
                }
            }    
        }
        
        public override string ToString()
        {
            return "Fighter";
        }

        virtual public void BattleCry()
        {
            Console.WriteLine("Хыа!");
        }
    }
}
