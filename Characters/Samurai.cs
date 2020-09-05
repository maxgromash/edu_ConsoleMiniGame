using System;

namespace Characters
{
    public class Samurai : Fighter
    {
        protected double retaliation;
        public double Retaliation
        {
            get
            {
                return retaliation;
            }
        }
        public Samurai(int hp, int damage, int guard, double evade, double retaliation)
            : base(hp, damage, guard, evade)
        {
            Health = hp;
            this.damage = damage;
            this.guard = guard;
            this.evade = evade;
            this.retaliation = retaliation;
        }

        /// <summary>
        /// Переопределенный метод атаки
        /// </summary>
        /// <param name="enemy">Ссылка на противника</param>
        override public void Attack(Human enemy)
        {
            TotalDamage = 0;
            var enm = (Fighter)enemy;

            if (poisoned)
            {
                if (Health * 0.93 >= 5) Health = Health * 0.93;
                else Health -= 5;
            }
            if (Health > 0)
            {
                if (rand.NextDouble() >= enm.Evade)
                {
                    TotalDamage = enm.GetDamage(damage);
                    if (enm.Guard > 0) enm.DownGuard();
                }
                else
                {
                    Console.WriteLine("Увернулся, подлец!");
                }
                if (enemy is Samurai && enemy.Health>0 && Health>0)
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
            return "Samurai";
        }
        override public void BattleCry()
        {
            Console.WriteLine("Чхуа!");
        }
    }
}