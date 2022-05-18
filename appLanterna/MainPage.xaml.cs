using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace appLanterna
{
    public partial class MainPage : ContentPage
    {
        string tipo = "off";
        public MainPage()
        {
            InitializeComponent();
            botao.Source = ImageSource.FromResource("appLanterna.onoff.off.png");
            
        }

        private void botao_Clicked(object sender, EventArgs e)
        {
            

            
            if (tipo == "off")
            {
                botao.Source = ImageSource.FromResource("appLanterna.onoff.on.png");
                tipo = "on";
            }
            else if(tipo == "on")
            {
                botao.Source = ImageSource.FromResource("appLanterna.onoff.off.png");
                tipo = "off";
            }

        }

        
    }
}
