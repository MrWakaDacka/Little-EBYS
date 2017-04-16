using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notBilgiSistemi
{
    public partial class NBS : Form
    {
        private List<Ders> dersler;
        private List<Ogrenci> ogrenciler;
        private Ders secilenDers;

        public NBS()
        {
            InitializeComponent();
            dersler = new List<Ders>();
            ogrenciler = new List<Ogrenci>();
            secilenDers = new Ders("NullExceptionStopped");
            dgsListView.View = View.Details;
            ogsListView.View = View.Details;

        }

        //TAB CONTROL EVENTS
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                dersSecimiComboBox.Items.Clear();
                ogrenciAtamaCheckedListBox.Items.Clear();
                ogrenciCikarmaListBox.Items.Clear();

                foreach (Ders d in dersler)
                {
                    dersSecimiComboBox.Items.Add(d.getAd());
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                dersSecimiNotComboBox.Items.Clear();
                ogrenciNotListBox.Items.Clear();
                projeNotYüzdesiTextBox.Text = "";
                vizeNotYüzdesiTextBox.Text = "";
                finalNotYüzdesiTextBox.Text = "";
                geçmeSınırıTextBox.Text = "";
                projeNotuNATextBox.Text = "";
                vizeNotuNATextBox.Text = "";
                finalNotuNATextBox.Text = "";
                bütünlemeNotuNATExtBox.Text = "";

                foreach (Ders d in dersler)
                {
                    dersSecimiNotComboBox.Items.Add(d.getAd());
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                dersSecimiDGSComboBox.Items.Clear();
                dgsListView.Items.Clear();

                foreach (Ders d in dersler)
                {
                    dersSecimiDGSComboBox.Items.Add(d.getAd());
                }
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                ogrenciSecimiOGSComboBox.Items.Clear();
                ogsListView.Items.Clear();

                foreach (Ogrenci o in ogrenciler)
                {
                    ogrenciSecimiOGSComboBox.Items.Add(o.getAd() + "-" + o.getNo());
                }
            }
        }

        //EKLEME / SİLME (1. TAB)
        private void dersEkleESButton_Click(object sender, EventArgs e)
        {
            if (dersEkleAdESTextBox.Text == "")
            {
                MessageBox.Show("Ders adı boş olamaz.");
                return;
            }
            string dersAdı = dersEkleAdESTextBox.Text;
            Boolean varmı = false;

            foreach (Ders d in dersler)
            {
                if (dersAdı.Equals(d.getAd(), StringComparison.InvariantCultureIgnoreCase))
                {
                    varmı = true;
                    break;
                }
            }

            if (varmı)
            {
                MessageBox.Show(dersAdı + " zaten mevcut!", "DERS OLUŞTURMA HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Ders d = new Ders(dersAdı);
                dersler.Add(d);
                dersESListBox.Items.Add(d.getAd());
            }
            dersEkleAdESTextBox.Text = "";
        }

        private void dersSilESButon_Click(object sender, EventArgs e)
        {
            foreach (string item in dersESListBox.CheckedItems.OfType<string>().ToList())
            {
                foreach (Ders d in dersler)
                {
                    if (d.getAd().Equals(item))
                    {
                        dersler.Remove(d);
                        break;
                    }
                }
                dersESListBox.Items.Remove(item);
            }
        }

        private void ogrenciEkleESButon_Click(object sender, EventArgs e)
        {
            string ad = ogrenciAdıESTextBox.Text;
            if (ad == "")
            {
                MessageBox.Show("Öğrenci ismi boş kalamaz!", "İSİM HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int no = 0;
            if (Int32.TryParse(ogrenciNumarasıESTextBox.Text, out no))
            {
                foreach (Ogrenci ogr in ogrenciler)
                {
                    if (ogr.getNo() == no)
                    {
                        MessageBox.Show("Bu numaralı öğrenci zaten mevcut!");
                        return;
                    }
                }
                Ogrenci o = new Ogrenci(no, ad);
                ogrenciler.Add(o);
                ad += "-" + no;
                ogrenciESListBox.Items.Add(ad);
                ogrenciAdıESTextBox.Text = "";
                ogrenciNumarasıESTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Öğrenci numarası rakamlardan oluşmak zorunda!", "ÖĞRENCİ NUMARASI HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ogrenciSilESButon_Click(object sender, EventArgs e)
        {
            foreach (string item in ogrenciESListBox.CheckedItems.OfType<string>().ToList())
            {
                int no = 0;
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == '-')
                    {
                        no = Int32.Parse(item.Substring(i + 1));
                        break;
                    }
                }

                ogrenciESListBox.Items.Remove(item);

                foreach (Ogrenci o in ogrenciler)
                {
                    if (o.getNo() == no)
                    {
                        ogrenciler.Remove(o);
                        break;
                    }
                }

                foreach (Ders d in dersler)
                {
                    d.deleteOgrenci(no);
                }
            }
        }

        //TAB 2 ÖĞRENCİ ATAMA

        private void OgrenciAtaButton_Click(object sender, EventArgs e)
        {
            Ogrenci ogrenci = new Ogrenci(0, "NullExceptionEngellemesi");
            Boolean varmı = false;

            if (dersSecimiComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Ders seçmeniz gerekli!", "DERS SEÇİMİ HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string item in ogrenciAtamaCheckedListBox.CheckedItems.OfType<string>().ToList())
            {
                int no = 0;
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == '-')
                    {
                        no = Int32.Parse(item.Substring(i + 1));
                        break;
                    }
                }
                foreach (Ogrenci o in ogrenciler)
                {
                    if (no == o.getNo())
                    {
                        ogrenci = o;
                        break;
                    }
                }

                foreach (Ogrenci aranacakOgrenci in secilenDers.getOgrenciList())
                {
                    if (ogrenci.getNo() == aranacakOgrenci.getNo())
                    {
                        varmı = true;
                        break;
                    }
                }

                if (!varmı)
                {
                    ogrenciCikarmaListBox.Items.Add(ogrenci.getAd() + "-" + ogrenci.getNo());
                    secilenDers.addOgrenci(ogrenci);
                    ogrenci.addNote(new DersNotu(secilenDers));
                }
                else
                {
                    varmı = false;
                }
            }
        }

        private void ogrenciCikarButton_Click(object sender, EventArgs e)
        {
            if (ogrenciCikarmaListBox.SelectedIndex == -1)
                return;

            foreach (string item in ogrenciCikarmaListBox.CheckedItems.OfType<string>().ToList())
            {
                Ogrenci ogrenci = new Ogrenci(0, "NullException");
                int no = 0;
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == '-')
                    {
                        no = Int32.Parse(item.Substring(i + 1));
                        break;
                    }
                }

                foreach (Ogrenci o in secilenDers.getOgrenciList())
                {
                    if (o.getNo() == no)
                    {
                        ogrenci = o;
                        break;
                    }
                }

                foreach (DersNotu dn in ogrenci.getNotlar())
                {
                    if (dn.getDers() == secilenDers)
                    {
                        ogrenci.removeNote(dn);
                        break;
                    }
                }

                secilenDers.deleteOgrenci(ogrenci);
                ogrenciCikarmaListBox.Items.Remove(item);
            }
        }

        private void dersSecimiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogrenciAtamaCheckedListBox.Items.Clear();
            ogrenciCikarmaListBox.Items.Clear();
            foreach (Ders d in dersler)
            {
                if (d.getAd().Equals(dersSecimiComboBox.SelectedItem.ToString()))
                {
                    secilenDers = d;
                    break;
                }
            }

            foreach (Ogrenci o in ogrenciler)
            {
                ogrenciAtamaCheckedListBox.Items.Add(o.getAd() + "-" + o.getNo());
            }
            foreach (Ogrenci o in secilenDers.getOgrenciList())
            {
                ogrenciCikarmaListBox.Items.Add(o.getAd() + "-" + o.getNo());
            }
        }

        // TAB 3 NOT ATAMA

        private void dersSecimiNotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            ogrenciNotListBox.Items.Clear();
            foreach (Ders d in dersler)
            {
                if (dersSecimiNotComboBox.SelectedItem.Equals(d.getAd()))
                {
                    secilenDers = d;
                    break;
                }
            }

            projeNotuNATextBox.Text = "";
            vizeNotuNATextBox.Text = "";
            finalNotuNATextBox.Text = "";
            bütünlemeNotuNATExtBox.Text = "";

            projeNotYüzdesiTextBox.Text = secilenDers.getProjeNotuYüzdesi().ToString();
            vizeNotYüzdesiTextBox.Text = secilenDers.getVizeNotuYüzdesi().ToString();
            finalNotYüzdesiTextBox.Text = secilenDers.getFinalNotuYüzdesi().ToString();
            geçmeSınırıTextBox.Text = secilenDers.getGeçmeSınırı().ToString();

            foreach (Ogrenci o in secilenDers.getOgrenciList())
            {
                ogrenciNotListBox.Items.Add(o.getAd() + "-" + o.getNo());
            }
        }

        private void ogrenciNotListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int no = 0;
            string item = ogrenciNotListBox.SelectedItem.ToString();

            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] == '-')
                {
                    no = Int32.Parse(item.Substring(i + 1));
                    break;
                }
            }

            foreach (Ogrenci o in secilenDers.getOgrenciList())
            {
                if (o.getNo() == no)
                {
                    foreach (DersNotu dn in o.getNotlar())
                    {
                        if (dn.getDers() == secilenDers)
                        {
                            projeNotuNATextBox.Text = dn.getProjeNotu().ToString();
                            vizeNotuNATextBox.Text = dn.getVizeNotu().ToString();
                            finalNotuNATextBox.Text = dn.getFinalNotu().ToString();
                            bütünlemeNotuNATExtBox.Text = dn.getBütünlemeNotu().ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void yüzdeleriAyarlaButton_Click(object sender, EventArgs e)
        {
            if (dersSecimiNotComboBox.SelectedIndex == -1)
                return;
            int proje = 0;
            int vize = 0;
            int final = 0;
            int geçmeSınırı = 0;
            if (!(Int32.TryParse(projeNotYüzdesiTextBox.Text, out proje)
                && Int32.TryParse(vizeNotYüzdesiTextBox.Text, out vize)
                && Int32.TryParse(finalNotYüzdesiTextBox.Text, out final)
                && Int32.TryParse(geçmeSınırıTextBox.Text, out geçmeSınırı)))
            {
                MessageBox.Show("Yüzdeler ve geçme sınırı rakamlardan oluşmak zorunda!", "GEÇERSİZ KARAKTER HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((proje < 0 || proje > 100) || (vize < 0 || vize > 100) || (final < 0 || final > 100))
            {
                MessageBox.Show("Yüzdeler 0-100 aralığında olmak zorunda!");
                return;
            }
            if ((proje + vize + final) != 100)
            {
                MessageBox.Show("Proje+Vize+Final Yüzdeleri Toplamı 100 olmak zorunda!");
                return;
            }
            if (geçmeSınırı < -1 || geçmeSınırı > 100)
            {
                MessageBox.Show("Geçme Sınırı (-1)-100 aralığında olmak zorunda!");
            }
            secilenDers.setProjeNotuYüzdesi(proje);
            secilenDers.setVizeNotuYüzdesi(vize);
            secilenDers.setFinalNotuYüzdesi(final);
            secilenDers.setGeçmeSınırı(geçmeSınırı);
        }

        private void notAtaButton_Click(object sender, EventArgs e)
        {
            if (dersSecimiNotComboBox.SelectedIndex == -1 || ogrenciNotListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Ders ve öğrenci seçilmeli", "ERİŞİM HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int proje = 0;
            int vize = 0;
            int final = 0;
            int bütünleme = 0;
            if (!(Int32.TryParse(projeNotuNATextBox.Text, out proje)
                && Int32.TryParse(vizeNotuNATextBox.Text, out vize)
                && Int32.TryParse(finalNotuNATextBox.Text, out final)
                && Int32.TryParse(bütünlemeNotuNATExtBox.Text, out bütünleme)))
            {
                MessageBox.Show("Notlar rakamlardan oluşmak zorunda!", "GEÇERSİZ KARAKTER HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((proje < -1 || proje > 100) || (vize < -1 || vize > 100) || (final < -1 || final > 100) || (bütünleme < -1 || bütünleme > 100))
            {
                MessageBox.Show("Notlar (-1)-100 aralığında olmak zorunda!");
                return;
            }

            int no = 0;
            string item = ogrenciNotListBox.SelectedItem.ToString();

            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] == '-')
                {
                    no = Int32.Parse(item.Substring(i + 1));
                    break;
                }
            }

            foreach (Ogrenci o in secilenDers.getOgrenciList())
            {
                if (o.getNo() == no)
                {
                    foreach (DersNotu dn in o.getNotlar())
                    {
                        if (dn.getDers() == secilenDers)
                        {
                            dn.setProjeNotu(proje);
                            dn.setVizeNotu(vize);
                            dn.setFinalNotu(final);
                            dn.setBütünlemeNotu(bütünleme);
                            return;
                        }
                    }
                }
            }
        }

        //TAB 4
        private void dersSecimiDGSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgsListView.BeginUpdate();
            foreach (Ders d in dersler)
            {
                if (d.getAd().Equals(dersSecimiDGSComboBox.SelectedItem.ToString()))
                {
                    dgsListView.Columns.Clear();
                    dgsListView.Items.Clear();
                    createHeadersForDGS(d);
                    secilenDers = d;
                    break;
                }
            }
            decimal çan = 0;
            int ogrSayı = 0;
            decimal projeKatkısı = 0;
            decimal vizeKatkısı = 0;
            decimal finalKatkısı = 0;
            if (secilenDers.getGeçmeSınırı() == -1)
            {
                foreach (Ogrenci o in secilenDers.getOgrenciList())
                {
                    foreach (DersNotu dn in o.getNotlar())
                    {
                        if (dn.getDers() == secilenDers)
                        {
                            if (dn.getProjeNotu() != -1)
                                projeKatkısı = (decimal)dn.getProjeNotu() * secilenDers.getProjeNotuYüzdesi() / 100;
                            if (dn.getVizeNotu() != -1)
                                vizeKatkısı = (decimal)dn.getVizeNotu() * secilenDers.getVizeNotuYüzdesi() / 100;
                            if (dn.getFinalNotu() != -1)
                                finalKatkısı = (decimal)dn.getFinalNotu() * secilenDers.getFinalNotuYüzdesi() / 100;
                            çan += projeKatkısı + vizeKatkısı + finalKatkısı;
                            break;
                        }
                    }
                    projeKatkısı = 0;
                    vizeKatkısı = 0;
                    finalKatkısı = 0;
                    ogrSayı++;
                }
                if (ogrSayı != 0)
                    çan = çan / (decimal)ogrSayı;
            }

            ListViewItem lvi;
            ListViewItem.ListViewSubItem lvsi;
            projeKatkısı = 0;
            vizeKatkısı = 0;
            finalKatkısı = 0;

            foreach (Ogrenci o in secilenDers.getOgrenciList())
            {
                foreach (DersNotu dn in o.getNotlar())
                {
                    if (dn.getDers() == secilenDers)
                    {

                        //Öğrenci No
                        lvi = new ListViewItem();
                        lvi.Text = "" + o.getNo();
                        lvi.ImageIndex = 0;

                        //Öğrenci İsmi
                        lvsi = new ListViewItem.ListViewSubItem();
                        lvsi.Text = o.getAd();
                        lvi.SubItems.Add(lvsi);

                        //Proje Notu
                        lvsi = new ListViewItem.ListViewSubItem();
                        if (dn.getProjeNotu() == -1 || secilenDers.getProjeNotuYüzdesi()==0)
                        {
                            lvsi.Text = "-";
                        }
                        else
                        {
                            projeKatkısı = (decimal)dn.getProjeNotu() * secilenDers.getProjeNotuYüzdesi() / 100;
                            lvsi.Text = "" + dn.getProjeNotu();
                        }
                        lvi.SubItems.Add(lvsi);

                        //Vize Notu
                        lvsi = new ListViewItem.ListViewSubItem();
                        if (dn.getVizeNotu() == -1)
                        {
                            lvsi.Text = "-";
                        }
                        else
                        {
                            vizeKatkısı = (decimal)dn.getVizeNotu() * secilenDers.getVizeNotuYüzdesi() / 100;
                            lvsi.Text = "" + dn.getVizeNotu();
                        }
                        lvi.SubItems.Add(lvsi);

                        //Final Notu
                        lvsi = new ListViewItem.ListViewSubItem();
                        if (dn.getFinalNotu() == -1)
                        {
                            lvsi.Text = "-";
                        }
                        else
                        {
                            finalKatkısı = (decimal)dn.getFinalNotu() * secilenDers.getFinalNotuYüzdesi() / 100;
                            lvsi.Text = "" + dn.getFinalNotu();
                        }
                        lvi.SubItems.Add(lvsi);

                        //Bütünleme Notu
                        lvsi = new ListViewItem.ListViewSubItem();
                        if (dn.getBütünlemeNotu() == -1)
                        {
                            lvsi.Text = "-";
                        }
                        else
                        {
                            finalKatkısı = (decimal)dn.getBütünlemeNotu() * secilenDers.getFinalNotuYüzdesi() / 100;
                            lvsi.Text = "" + dn.getBütünlemeNotu();
                        }
                        lvi.SubItems.Add(lvsi);

                        //Ortalama
                        lvsi = new ListViewItem.ListViewSubItem();
                        lvsi.Text = "" + (projeKatkısı + vizeKatkısı + finalKatkısı);
                        lvi.SubItems.Add(lvsi);

                        //Geçme Sınırı
                        lvsi = new ListViewItem.ListViewSubItem();
                        if (secilenDers.getGeçmeSınırı() == -1)
                        {
                            lvsi.Text = çan.ToString("#.##");
                        }
                        else lvsi.Text = "" + secilenDers.getGeçmeSınırı();
                        lvi.SubItems.Add(lvsi);

                        //Geçme Durumu
                        lvsi = new ListViewItem.ListViewSubItem();
                        if ((secilenDers.getProjeNotuYüzdesi() != 0 && dn.getProjeNotu() == -1)
                            || (dn.getVizeNotu() == -1) || (dn.getFinalNotu() == -1))
                        {
                            lvsi.Text = "-";
                        }
                        else if (secilenDers.getGeçmeSınırı() == -1)
                        {
                            if ((projeKatkısı + vizeKatkısı + finalKatkısı) > çan)
                                 lvsi.Text = "Başarılı";
                            else lvsi.Text = "Başarısız";
                        }
                        else if ((projeKatkısı + vizeKatkısı + finalKatkısı) > secilenDers.getGeçmeSınırı())
                        {
                            lvsi.Text = "Başarılı";
                        }
                        else lvsi.Text = "Başarısız";
                        lvi.SubItems.Add(lvsi);
                        dgsListView.Items.Add(lvi);
                        break;
                    }
                }
            }
            dgsListView.EndUpdate();
        }

        //TAB 5
        private void dersSecimiOGSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int no = 0;
            string item = ogrenciSecimiOGSComboBox.SelectedItem.ToString();

            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] == '-')
                {
                    no = Int32.Parse(item.Substring(i + 1));
                    break;
                }
            }

            ogsListView.BeginUpdate();

            Ogrenci secilenOgrenci = new Ogrenci(0, "Nullexception");
            foreach (Ogrenci o in ogrenciler)
            {
                if (o.getNo() == no)
                {
                    ogsListView.Columns.Clear();
                    ogsListView.Items.Clear();
                    createHeadersForOGS();
                    secilenOgrenci = o;
                    break;
                }
            }
            decimal çan = 0;
            int ogrSayisi = 0;
            decimal çanProjeKatkısı = 0;
            decimal çanVizeKatkısı = 0;
            decimal çanFinalKatkısı = 0;
            decimal projeKatkısı = 0;
            decimal vizeKatkısı = 0;
            decimal finalKatkısı = 0;
            ListViewItem lvi;
            ListViewItem.ListViewSubItem lvsi;


            foreach (DersNotu dn in secilenOgrenci.getNotlar())
            {
                secilenDers = dn.getDers();
                if (secilenDers.getGeçmeSınırı() == -1)
                {
                    foreach (Ogrenci o in secilenDers.getOgrenciList())
                    {
                        foreach (DersNotu drsNt in o.getNotlar())
                        {
                            if (drsNt.getDers() == secilenDers)
                            {
                                if (drsNt.getProjeNotu() != -1)
                                    çanProjeKatkısı = (decimal)drsNt.getProjeNotu() * secilenDers.getProjeNotuYüzdesi() / 100;
                                if (drsNt.getVizeNotu() != -1)
                                    çanVizeKatkısı = (decimal)drsNt.getVizeNotu() * secilenDers.getVizeNotuYüzdesi() / 100;
                                if (drsNt.getFinalNotu() != -1)
                                    çanFinalKatkısı = (decimal)drsNt.getFinalNotu() * secilenDers.getFinalNotuYüzdesi() / 100;
                                çan += çanProjeKatkısı + çanVizeKatkısı + çanFinalKatkısı;
                                break;
                            }
                        }
                        ogrSayisi++;
                        çanProjeKatkısı = 0;
                        çanVizeKatkısı = 0;
                        çanFinalKatkısı = 0;
                    }
                    if (ogrSayisi != 0)
                        çan /= (decimal)ogrSayisi;
                }

                //Ders
                lvi = new ListViewItem();
                lvi.Text = secilenDers.getAd();
                lvi.ImageIndex = 0;

                //Proje Notu
                lvsi = new ListViewItem.ListViewSubItem();
                if (dn.getProjeNotu() == -1|| secilenDers.getProjeNotuYüzdesi()==0)
                {
                    lvsi.Text = "-";
                }
                else
                {
                    projeKatkısı = (decimal)dn.getProjeNotu() * secilenDers.getProjeNotuYüzdesi() / 100;
                    lvsi.Text = "" + dn.getProjeNotu();
                }
                lvi.SubItems.Add(lvsi);

                //Vize Notu
                lvsi = new ListViewItem.ListViewSubItem();
                if (dn.getVizeNotu() == -1)
                {
                    lvsi.Text = "-";
                }
                else
                {
                    vizeKatkısı = (decimal)dn.getVizeNotu() * secilenDers.getVizeNotuYüzdesi() / 100;
                    lvsi.Text = "" + dn.getVizeNotu();
                }
                lvi.SubItems.Add(lvsi);

                //Final Notu
                lvsi = new ListViewItem.ListViewSubItem();
                if (dn.getFinalNotu() == -1)
                {
                    lvsi.Text = "-";
                }
                else
                {
                    finalKatkısı = (decimal)dn.getFinalNotu() * secilenDers.getFinalNotuYüzdesi() / 100;
                    lvsi.Text = "" + dn.getFinalNotu();
                }
                lvi.SubItems.Add(lvsi);

                //Bütünleme Notu
                lvsi = new ListViewItem.ListViewSubItem();
                if (dn.getBütünlemeNotu() == -1)
                {
                    lvsi.Text = "-";
                }
                else
                {
                    finalKatkısı = (decimal)dn.getBütünlemeNotu() * secilenDers.getFinalNotuYüzdesi() / 100;
                    lvsi.Text = "" + dn.getBütünlemeNotu();
                }
                lvi.SubItems.Add(lvsi);

                //Ortalama
                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = "" + (projeKatkısı + vizeKatkısı + finalKatkısı);
                lvi.SubItems.Add(lvsi);

                //Geçme Sınırı
                lvsi = new ListViewItem.ListViewSubItem();
                if (secilenDers.getGeçmeSınırı() == -1)
                {
                    lvsi.Text = çan.ToString("#.##");
                }
                else lvsi.Text = "" + secilenDers.getGeçmeSınırı();
                lvi.SubItems.Add(lvsi);

                //Geçme Durumu
                lvsi = new ListViewItem.ListViewSubItem();
                if ((secilenDers.getProjeNotuYüzdesi() != 0 && dn.getProjeNotu() == -1)
                    || (dn.getVizeNotu() == -1) || (dn.getFinalNotu() == -1))
                {
                    lvsi.Text = "-";
                }
                else if (secilenDers.getGeçmeSınırı() == -1)
                {
                    if ((projeKatkısı + vizeKatkısı + finalKatkısı) > çan)
                         lvsi.Text = "Başarılı";
                    else lvsi.Text = "Başarısız";
                }
                else if ((projeKatkısı + vizeKatkısı + finalKatkısı) > secilenDers.getGeçmeSınırı())
                {
                    lvsi.Text = "Başarılı";
                }
                else lvsi.Text = "Başarısız";
                lvi.SubItems.Add(lvsi);

                ogsListView.Items.Add(lvi);

            }

            ogsListView.EndUpdate();
        }

        //ÖZEL FONKSİYONLAR

        public void createHeadersForDGS(Ders d)
        {
            dgsListView.Columns.Add("Öğrenci No", 100, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Öğrenci İsmi", 100, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Proje Notu (%" + d.getProjeNotuYüzdesi() + ")", 90, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Vize Notu (%" + d.getVizeNotuYüzdesi() + ")", 90, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Final Notu (%" + d.getFinalNotuYüzdesi() + ")", 90, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Bütünleme Notu", 100, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Ortalama", 80, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Geçme Sınırı", 80, HorizontalAlignment.Center);
            dgsListView.Columns.Add("Geçme Durumu", 100, HorizontalAlignment.Center);
        }

        public void createHeadersForOGS()
        {


            ogsListView.Columns.Add("Ders", 100, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Proje Notu", 90, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Vize Notu", 90, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Final Notu", 90, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Bütünleme Notu", 100, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Ortalama", 80, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Geçme Sınırı", 80, HorizontalAlignment.Center);
            ogsListView.Columns.Add("Geçme Durumu", 100, HorizontalAlignment.Center);
        }


    }
}
