Public Class Rotor
    Public ReadOnly Mappings As Dictionary(Of Char, Char) = New Dictionary(Of Char, Char)
    Public ReadOnly Notches() As Char
    Private _Offset As Integer = 0

    Public Property Offset() As Integer
        Get
            Return _Offset
        End Get
        Set(ByVal value As Integer)
            _Offset = value
        End Set
    End Property


    Public Sub New(ByRef Mappings As Dictionary(Of Char, Char), ByVal Notches() As Char)
        Me.Mappings = Mappings
        Me.Notches = Notches
    End Sub


    Public Sub Map(ByVal Char1 As Char, ByVal Char2 As Char)
        Me.Mappings(Char1) = Char2
    End Sub

    Public Sub Rotate()

        Dim MappingSources(Mappings.Keys.Count - 1) As Char
        Dim MappingDestinations(Mappings.Values.Count - 1) As Char

        Mappings.Keys.CopyTo(MappingSources, 0)
        Mappings.Values.CopyTo(MappingDestinations, 0)


        Dim FirstDestination As Char = MappingDestinations(0)
        For i As Integer = 0 To MappingDestinations.Count - 2
            Me.Map(MappingSources(i), MappingDestinations(i + 1))
        Next
        Me.Map(MappingSources.Last(), FirstDestination)

        Me.Offset = (Me.Offset + 1) Mod Me.Mappings.Count

    End Sub

    Public Function StandardEncrypt(ByVal Char1 As Char)
        If Me.Mappings.ContainsKey(Char1) Then
            Return Me.Mappings(Char1)
        End If
        Throw New TranslationException("Could not map " & Char1 & " to any character.")
    End Function

    Public Function ReflectEncrypt(ByVal Char1 As Char)
        For Each Pair As KeyValuePair(Of Char, Char) In Me.Mappings
            If Pair.Value = Char1 Then
                Return Pair.Key
            End If
        Next
        Throw New TranslationException("Could not map " & Char1 & " to any character.")
    End Function
End Class

Class TranslationException
    Inherits Exception

    Public Sub New()

    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class
