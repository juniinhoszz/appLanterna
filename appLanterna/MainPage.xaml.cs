using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Plugin.Battery;
using Xamarin.Essentials;

namespace appLanterna
{
    public partial class MainPage : ContentPage
    {
        string tipo = "off";
        public MainPage()
        {
            InitializeComponent();
            botao.Source = ImageSource.FromResource("appLanterna.onoff.off.png");

            Carrega_Informacoes_Bateria();

        }

        private async void botao_Clicked(object sender, EventArgs e)
        {
            

            
            if (tipo == "off")
            {
                botao.Source = ImageSource.FromResource("appLanterna.onoff.on.png");
                tipo = "on";
                Vibration.Vibrate(TimeSpan.FromMilliseconds(250));
                await Flashlight.TurnOnAsync();
            }
            else if(tipo == "on")
            {
                botao.Source = ImageSource.FromResource("appLanterna.onoff.off.png");
                tipo = "off";
                Vibration.Vibrate(TimeSpan.FromMilliseconds(250));
                await Flashlight.TurnOffAsync();
            }

        }

        private void Carrega_Informacoes_Bateria()
        {
            try
            {
                if (CrossBattery.IsSupported)
                {
                    CrossBattery.Current.BatteryChanged -= Mudanca_Status_Bateria;
                    CrossBattery.Current.BatteryChanged += Mudanca_Status_Bateria;
                }

            }catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private void Mudanca_Status_Bateria(object sender, Plugin.Battery.Abstractions.BatteryChangedEventArgs e)
        {
            try
            {
                lbl_porc_restante.Text = e.RemainingChargePercent.ToString() + "%";

                if (e.IsLow)
                {
                    lbl_bateria_fraca.Text = "✘ A Bateria Está Fraca!";
                }
                else
                {
                    lbl_bateria_fraca.Text = "";
                }

                switch (e.Status)
                {
                    case Plugin.Battery.Abstractions.BatteryStatus.Charging:
                        lbl_status.Text = "Carregando";
                    break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Discharging:
                        lbl_status.Text = "Descarregando";
                    break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Full:
                        lbl_status.Text = "Carregada";
                    break;

                    case Plugin.Battery.Abstractions.BatteryStatus.NotCharging:
                        lbl_status.Text = "Sem Carregar";
                    break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Unknown:
                        lbl_status.Text = "Desconhecido";
                    break;
                }

                switch(e.PowerSource)
                {
                    case Plugin.Battery.Abstractions.PowerSource.Ac:
                        lbl_fonte_carregamento.Text = "Carregador";
                    break;

                    case Plugin.Battery.Abstractions.PowerSource.Battery:
                        lbl_fonte_carregamento.Text = "Bateria";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Usb:
                        lbl_fonte_carregamento.Text = "USB";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Wireless:
                        lbl_fonte_carregamento.Text = "Sem Fio";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Other:
                        lbl_fonte_carregamento.Text = "Desconhecida";
                        break;
                }


            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        
    }
}
