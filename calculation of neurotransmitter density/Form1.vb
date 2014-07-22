Public Class Form1

    Dim NSt = 0

    Dim Nsource As Integer = 25000

    Dim Nneuron As Integer = 650

    Dim Ncatch As Integer = 15
    Dim RSC1 As Integer
    Dim RSC2 As Integer


    Dim NSignals As Integer = 10




    Dim Dendrit(Nsource - 1) As Integer

    Dim Pattern(Nneuron - 1) As Boolean

    Dim AVG(Ncatch - 1) As Integer

    Dim DistrP(Ncatch - 1) As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        TextBox5.Text = "Nsource=" & Nsource.ToString & vbCrLf & "Nneuron=" & Nneuron.ToString & vbCrLf & "Ncatch =" & Ncatch.ToString & vbCrLf & "Nsig =" & NSignals.ToString


        If Ncatch Mod 2 = 0 Then

            RSC1 = Ncatch / 2
            RSC2 = RSC1 - 1


        Else

            RSC1 = (Ncatch - 1) / 2
            RSC2 = RSC1

        End If



        ConnectionCreate()

    End Sub

    Private Sub ConnectionCreate()

        For i = 0 To Nsource - 1

            Dendrit(i) = Rnd() * (Nneuron - 1)

        Next

    End Sub

    Private Sub PatternCreate()

        Dim N, k As Integer

        For i = 0 To Nneuron - 1

            Pattern(i) = False

        Next

        k = 0

        Do While k < NSignals

            N = Rnd() * (Nneuron - 1)

            If Not Pattern(N) Then
                Pattern(N) = True
                k += 1
            End If

        Loop



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        St()
    End Sub

    Private Sub St()

        PatternCreate()

        Dim result(Nsource - 1) As Integer
        Dim Distr(Ncatch - 1) As Integer

        Dim C As Integer = 0

        For i = 0 To Ncatch - 1
            If Pattern(Dendrit(i)) Then C += 1
        Next

        For i = RSC1 To Nsource - RSC2 - 1

            result(i) = C

            Distr(C) += 1

            If Pattern(Dendrit(i - RSC1)) Then C -= 1
            If Pattern(Dendrit(i + RSC2)) Then C += 1

        Next

        For i = 0 To Ncatch - 1

            If Distr(i) > 0 Then DistrP(i) += 1

        Next

        For i = 0 To Ncatch - 1

            AVG(i) += Distr(i)

        Next

        NSt += 1
        Label1.Text = NSt.ToString
        Label1.Update()

        Dim str1 As String = ""
        Dim str2 As String = ""
        Dim str3 As String = ""
        Dim str4 As String = ""

        For i = 0 To Ncatch - 1

            str1 &= i.ToString & vbCrLf
            str2 &= Distr(i).ToString & vbCrLf

            If DistrP(i) <> 0 Then
                str3 &= ((AVG(i) / DistrP(i)).ToString & "     ").Substring(0, 5) & vbCrLf
            Else
                str3 &= ("0" & vbCrLf)
            End If


            str4 &= ((DistrP(i) / NSt).ToString & "     ").Substring(0, 5) & vbCrLf

        Next

        TextBox1.Text = str1
        TextBox1.Update()
        TextBox2.Text = str2
        TextBox2.Update()
        TextBox3.Text = str3
        TextBox3.Update()
        TextBox4.Text = str4
        TextBox4.Update()

    End Sub




    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        NSt = 0
        For i = 0 To Ncatch - 1

            DistrP(i) = 0

        Next

        PatternCreate()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For i = 0 To 99
            St()
        Next
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For i = 0 To 3999
            St()
        Next
    End Sub
End Class
