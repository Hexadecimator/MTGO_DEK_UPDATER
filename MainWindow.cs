using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace MTGO_dek_FIXER
{
    public partial class MainWindow : Form
    {
        /*
         * wishlist:
         * 1. when multiple versions detected as user which one is preferred
         *      - when reading in master collection file
         *      - for now just use first version available
         */
        bool master_collection_detected = false;
        string[] master_collection_contents;
        
        Deck MTGO_MASTER_COLLECTION; // this holds the master collection deserialized xml contents
        public bool deserializeXML_fromMasterCollection(string collection_path)
        {
            try
            {
                XmlSerializer MTGO_collection_xml = new XmlSerializer(typeof(Deck));
                using (Stream reader = new FileStream(collection_path, FileMode.Open))
                {
                    // Call the Deserialize method to read the collection data into the object
                    MTGO_MASTER_COLLECTION = (Deck)MTGO_collection_xml.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                lblSTATUS.Text = "Error deserializing master collection xml";
                master_collection_detected = false;
                return false;
            }
            return true;
        }

        string current_deck_name = "nodeckname";
        Deck MTGO_IMPORTED_DECK; // this holds the master collection deserialized xml contents
        public bool deserializeXML_fromImportedDecklist(string collection_path)
        {
            try
            {
                XmlSerializer MTGO_import_deck_xml = new XmlSerializer(typeof(Deck));
                using (Stream reader = new FileStream(collection_path, FileMode.Open))
                {
                    // Call the Deserialize method to read the collection data into the object
                    MTGO_IMPORTED_DECK = (Deck)MTGO_import_deck_xml.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                lblSTATUS.Text = "Error deserializing import deck xml";
                return false;
            }
            return true;
        }

        public bool checkForMasterCollection()
        {
            bool detected = false;
            // check for an existing file next to the .exe named "MCF.cards"
            try
            {
                master_collection_contents = File.ReadAllLines("MCF.cards");
                if (master_collection_contents.Length > 0) { /* good */ }
                else
                {
                    lblSTATUS.Text = "Master collection file is empty!";
                    return detected;
                }
            }
            catch(Exception ex)
            {
                lblSTATUS.Text = $"Could not open master collection file.\r\n\r\nPlease import new master collection file.";
                return detected;
            }

            // we have successfully detected the file and it has actual contents
            // so try to deserialize it
            if (!deserializeXML_fromMasterCollection("MCF.cards")) return false;
            detected = true;
            lblSTATUS.Text = "Loaded master collection file.";
            return detected;
        }

        public void updateDeckToContainOnlyPurchasedCards()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Select .dek File",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "dek",
                Filter = "MTGO files (*.txt)|*.txt|(*.dek)|*.dek",
                FilterIndex = 2,
                RestoreDirectory = true,

                //ReadOnlyChecked = true,
                //ShowReadOnly = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lblSTATUS.Text = $"Selected import deck file: {ofd.FileName}";
                current_deck_name = Path.GetFileName(ofd.FileName);
                current_deck_name = current_deck_name.Replace(".dek", "");
                current_deck_name = current_deck_name.Replace(" ", "_");
                current_deck_name = $"converted_{current_deck_name}";
                //lblFILENAME.Text = current_deck_name; // debug
            }
            else
            {
                lblSTATUS.Text = "Deck import cancelled!";
                return;
            }

            if (!deserializeXML_fromImportedDecklist(ofd.FileName)) return; 

            // now update all the imported card's CatIDs to CatIDs that are owned by the user
            foreach(Cards import_card in MTGO_IMPORTED_DECK.Cards)
            {
                foreach(Cards collection_card in MTGO_MASTER_COLLECTION.Cards)
                {
                    if(import_card.Name.ToLower() == collection_card.Name.ToLower())
                    {
                        import_card.CatID = collection_card.CatID;
                        // debug
                        //lblSTATUS.Text = $"Replaced {collection_card.Name} CatIDs";
                        break;
                    }
                }
            }

            // at thise point MTGO_IMPORTED_DECK's CatID's should be updated, reserialize it to a .dek file
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Deck));

            string path = $"{Environment.CurrentDirectory}\\1_CONVERTED";
            Directory.CreateDirectory(path);

            string filename = $"{current_deck_name}_{DateTime.Now.ToString("MM_dd_yyyy-HH_mm_ss")}.dek";

            string fullpath = System.IO.Path.Combine(path, filename);

            System.IO.FileStream file = System.IO.File.Create(fullpath);
            writer.Serialize(file, MTGO_IMPORTED_DECK);
            file.Close();
            lblSTATUS.Text = $"Processed decklist here:\r\n{fullpath}";
        }

        public MainWindow()
        {
            InitializeComponent();

            // see if there is a collection present
            master_collection_detected = checkForMasterCollection();
        }

        private void btnPARSE_Click(object sender, EventArgs e)
        {
            if(!master_collection_detected)
            {
                lblSTATUS.Text = "No master collection file detected!\r\nImport one!";
                return;
            }

            updateDeckToContainOnlyPurchasedCards();
        }



        private void btnCOLLECTION_Click(object sender, EventArgs e)
        {
            // give a chance to back out if a collection is already present
            if(master_collection_detected)
            {
                DialogResult res = MessageBox.Show("Confirm Collection Overwrite?", "Confirm Overwrite", MessageBoxButtons.YesNo);
                if(res == DialogResult.No)
                {
                    return;
                }
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Select .dek Collection File",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "dek",
                Filter = "MTGO files (*.txt)|*.txt|(*.dek)|*.dek",
                FilterIndex = 2,
                RestoreDirectory = true,

                //ReadOnlyChecked = true,
                //ShowReadOnly = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lblSTATUS.Text = $"Selected collection file: {ofd.FileName}";
            }
            else
            {
                lblSTATUS.Text = "Collection selection cancelled!";
                return;
            }

            if(!deserializeXML_fromMasterCollection(ofd.FileName)) return; // can't go any further
            master_collection_detected = true;
            // debug
            //lblSTATUS.Text = $"MTGO_MASTER_COLLECTION.Cards[0].CatID = {MTGO_MASTER_COLLECTION.Cards[0].CatID}\r\nMTGO_MASTER_COLLECTION.Cards[0].Name = { MTGO_MASTER_COLLECTION.Cards[0].Name}";

            string master_output_path = Environment.CurrentDirectory + "/MCF.cards";
            System.IO.File.Copy(ofd.FileName, master_output_path, true); // create the MCF.cards file for later. Overwrite if already existing
            lblSTATUS.Text = "Master collection file processing complete.";
        }
    }
}
