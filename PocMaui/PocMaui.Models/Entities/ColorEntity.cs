using PocMaui.Models.DTOs.Down;
using SQLite;

namespace PocMaui.Models.Entities
{
    public class ColorEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexaCode { get; set; }
        public DateTime CreationDate { get; set; }

        public ColorEntity()
        {
            CreationDate = DateTime.Now;
        }

        public ColorEntity(string name, string hexaCode):this()
        {
            Name = name;
            HexaCode = hexaCode;
        }

        public ColorEntity(ColorProperty colorProperty):this()
        {
            Name = colorProperty.Hex.ToUpper();
            HexaCode = colorProperty.Hex.ToUpper();
        }
    }
}
