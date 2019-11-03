using System.Collections.Generic;
using System.Xml.Serialization;

namespace DAR.API.Model
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    [XmlRoot(ElementName = "value")]
    public class Value
    {
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "is")]
        public string Is { get; set; }
        [XmlAttribute(AttributeName = "from")]
        public string From { get; set; }
        [XmlAttribute(AttributeName = "to")]
        public string To { get; set; }
        [XmlAttribute(AttributeName = "num")]
        public string Num { get; set; }
    }

    [XmlRoot(ElementName = "domain")]
    public class Domain
    {
        public int Id { get; set; }
        [XmlElement(ElementName = "value")]
        public List<Value> Values { get; set; }
        [XmlAttribute(AttributeName = "ordered")]
        public string Ordered { get; set; }
    }

    [XmlRoot(ElementName = "type")]
    public class Type
    {
        [XmlElement(ElementName = "desc")]
        public string Description { get; set; }
        [XmlElement(ElementName = "domain")]
        public Domain Domain { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "base")]
        public string Base { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "scale")]
        public string Scale { get; set; }
    }


    [XmlRoot(ElementName = "attr")]
    public class Attribute
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "clb")]
        public string CLB { get; set; }
        [XmlAttribute(AttributeName = "abbrev")]
        public string Abbreviation { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlAttribute(AttributeName = "comm")]
        public string Comm { get; set; }
        [XmlElement(ElementName = "desc")]
        public string Description { get; set; }
    }


    [XmlRoot(ElementName = "attref")]
    public class Reference
    {
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "ref")]
        public string Destination { get; set; }
    }

    [XmlRoot(ElementName = "property")]
    public class Property
    {
        [XmlElement(ElementName = "attref")]
        public List<Reference> References { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }



    [XmlRoot(ElementName = "trans")]
    public class Transformation
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "src")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "dst")]
        public string Destination { get; set; }
    }


    [XmlRoot(ElementName = "dep")]
    public class Dependency
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "independent")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "dependent")]
        public string Destination { get; set; }
    }



    [XmlRoot(ElementName = "hml")]
    public class HML
    {
        public int Id { get; set; }
        [XmlArray(ElementName = "type_set")]

        [XmlArrayItem(ElementName = "type")]
        public List<Type> Types { get; set; }

        [XmlArray(ElementName = "attribute_set")]
        [XmlArrayItem(ElementName = "attr")]

        public List<Attribute> Attributes { get; set; }

        [XmlArray(ElementName = "property_set")]
        [XmlArrayItem(ElementName = "property")]
        public List<Property> Properties { get; set; }


        [XmlArray(ElementName = "tph")]
        [XmlArrayItem(ElementName = "trans")]
        public List<Transformation> TPH { get; set; }

        [XmlArray(ElementName = "ard")]
        [XmlArrayItem(ElementName = "dep")]
        public List<Dependency> ARD { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}
