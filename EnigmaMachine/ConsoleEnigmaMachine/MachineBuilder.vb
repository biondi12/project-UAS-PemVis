Imports System.Xml
Module MachineBuilder

    Public Function ReflectorFromXMLFilePath(ByVal Path As String) As Dictionary(Of Char, Char)
        Dim XMLDoc As New XmlDocument
        XMLDoc.Load(Path)
        Return ReflectorFromXMLDocument(XMLDoc)
    End Function

    Public Function ReflectorFromXMLData(ByVal XMLData As String) As Dictionary(Of Char, Char)
        Dim XMLDoc As New XmlDocument
        XMLDoc.LoadXml(XMLData)
        Return ReflectorFromXMLDocument(XMLDoc)
    End Function

    Public Function ReflectorFromXMLDocument(ByRef XMLDoc As XmlDocument) As Dictionary(Of Char, Char)
        Dim ReflectorNode As XmlNode = XMLDoc.Item("reflector")
        Return MappingsFromXML(ReflectorNode)
    End Function

    Public Function RotorsFromXMLFilePath(ByVal Path As String) As Rotor()
        Dim XMLDoc As New XmlDocument
        XMLDoc.Load(Path)
        Return RotorsFromXMLDocument(XMLDoc)
    End Function

    Public Function RotorsFromXMLData(ByVal XMLData As String) As Rotor()
        Dim XMLDoc As New XmlDocument
        XMLDoc.LoadXml(XMLData)
        Return RotorsFromXMLDocument(XMLDoc)
    End Function

    Public Function RotorsFromXMLDocument(ByRef XMLDoc As XmlDocument) As Rotor()
        Dim RotorNodes As XmlNodeList = XMLDoc.GetElementsByTagName("rotor")
        Dim Rotors As New List(Of Rotor)
        
        For Each RotorNode As XmlNode In RotorNodes
            If IsNothing(RotorNode.Attributes.ItemOf("position")) Then
                Dim Notches() As Char = RotorNode.Attributes.ItemOf("notches").Value.ToCharArray()
                Rotors.Add(New Rotor(MappingsFromXML(RotorNode), Notches))
            End If
        Next

        For Each RotorNode As XmlNode In RotorNodes
            If Not IsNothing(RotorNode.Attributes.ItemOf("position")) Then
                Dim Notches() As Char = RotorNode.Attributes.ItemOf("notches").Value.ToCharArray()
                Dim Position As Integer = Val(RotorNode.Attributes.ItemOf("position").Value) - 1
                Rotors.Insert(Position, New Rotor(MappingsFromXML(RotorNode), Notches))
            End If
        Next
        Return Rotors.ToArray()
    End Function

    Private Function MappingsFromXML(ByRef ParentNode As XmlNode) As Dictionary(Of Char, Char)
        Dim Mappings As New Dictionary(Of Char, Char)
        For Each MappingNode As XmlNode In ParentNode.ChildNodes
            Dim Source, Destination As Char
            Source = MappingNode.Attributes.ItemOf("source").Value
            Destination = MappingNode.Attributes.ItemOf("destination").Value
            Mappings(Source) = Destination
        Next
        Return Mappings
    End Function

End Module