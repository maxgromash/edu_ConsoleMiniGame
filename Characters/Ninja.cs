using System;

namespace Characters
{
    public class Ninja : Fighter
    {
        protected double poison;
        public Ninja(int hp, int damage, int guard, double evade, double poison)
            : base(hp, damage, guard, evade)
        {
            Health = hp;
            this.damage = damage;
            this.guard = guard;
            this.evade = evade;
            this.poison = poison;
        }

        /// <summary>
        /// Переопределенный метод атаки
        /// </summary>
        /// <param name="enemy"></param>
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
                    if (rand.NextDouble() < poison) enemy.IsPoisoned();
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
                        Console.WriteLine("Самурай наносит ответный удар!");
                        emnS.Attack(this);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Ninja";
        }
        override public void BattleCry()
        {
            Console.WriteLine("Киа!");
        }
    }
}