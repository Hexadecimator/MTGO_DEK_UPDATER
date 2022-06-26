using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MTGO_dek_FIXER
{
    public partial class MainWindow
    {
		[XmlRoot(ElementName = "Cards")]
		public class Cards
		{
			[XmlAttribute(AttributeName = "CatID")]
			public string CatID { get; set; }
			[XmlAttribute(AttributeName = "Quantity")]
			public string Quantity { get; set; }
			[XmlAttribute(AttributeName = "Sideboard")]
			public string Sideboard { get; set; }
			[XmlAttribute(AttributeName = "Name")]
			public string Name { get; set; }
			[XmlAttribute(AttributeName = "Annotation")]
			public string Annotation { get; set; }
		}

		[XmlRoot(ElementName = "Deck")]
		public class Deck
		{
			[XmlElement(ElementName = "NetDeckID")]
			public string NetDeckID { get; set; }
			[XmlElement(ElementName = "PreconstructedDeckID")]
			public string PreconstructedDeckID { get; set; }
			[XmlElement(ElementName = "Cards")]
			public List<Cards> Cards { get; set; }
		}
	}
}
