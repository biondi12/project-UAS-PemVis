Public Class EnigmaMachine
    Public Rotors() As Rotor
    Public Reflector As Dictionary(Of Char, Char)

    Public Sub New(ByVal Rotors() As Rotor, ByVal Reflector As Dictionary(Of Char, Char))


        Me.Rotors = Rotors
        Me.Reflector = Reflector
    End Sub

    Public Sub RotateRotors()
        
        Me.Rotors.Last().Rotate()

        For i As Integer = UBound(Me.Rotors) - 1 To 0 Step -1
            If Me.Rotors(i + 1).Notches.Contains(Me.Rotors(i + 1).Mappings.Values.Last()) Then
                Me.Rotors(i).Rotate()
            End If
        Next
    End Sub

    Public Function EncryptChar(ByVal PlainChar As Char)
        Dim CipherChar As Char = PlainChar
        Me.RotateRotors()
        For Each Rot As Rotor In Me.Rotors.Reverse()
            CipherChar = Rot.StandardEncrypt(CipherChar)
        Next
        CipherChar = Reflector(CipherChar)
        For Each Rot As Rotor In Me.Rotors
            CipherChar = Rot.ReflectEncrypt(CipherChar)
        Next

        Return CipherChar
    End Function

    Public Function EncryptText(ByVal PlainText As String)
        Dim CipherText As String = ""
        For Each PlainChar As Char In PlainText
            CipherText += Me.EncryptChar(PlainChar)
        Next
        Return CipherText
    End Function

End Class
