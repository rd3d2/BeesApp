using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Bee> list = new List<Bee>(0);
            listView.ItemsSource = CreateNewBees(list);
        }


        private static List<Bee> CreateNewBees(List<Bee> list)
        {
            list.Clear();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Worker());
                list.Add(new Drone());
                list.Add(new Queen());
            }
            return list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            for (int i = 0; i < (this.listView.Items.Count); i++)
            {
                var damage = random.Next(0, 80);
                ((Bee)(this.listView.Items[i])).Damage(damage);
            }
            this.listView.Items.Refresh();

        }
    }

    public abstract class Bee
    {
        public virtual string Type { get; protected set; }
        public virtual float Health { get; protected set; }
        protected int HealthLimit { get; set; }

        public virtual bool Dead
        {
            get
            {
                return (this.Health < this.HealthLimit);
            }
        }

        public virtual void Damage(int Damage)
        {
            if (!this.Dead && Damage > 0 && Damage < 100)
            {
                this.Health -= Damage;
            }
            if (this.Health < 0)
            {
                this.Health = 0;
            }
        }

        protected Bee(string type, int healthLimit)
        {
            this.Type = type;
            this.Health = 100;
            this.HealthLimit = healthLimit;
        }
    }
    public sealed class Worker : Bee { public Worker() : base("Worker", 70) { }}
    public sealed class Drone : Bee { public Drone() : base("Drone", 50) { } }
    public sealed class Queen : Bee { public Queen() : base("Queen", 20) { } }

}


