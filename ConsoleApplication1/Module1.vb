Imports QRCoder
Imports System.IO
Imports System.Drawing
Imports System.Windows
Imports System.Data.SqlClient

Module Module1
    Public CadenaConexion As String = "Data Source=SERVER;Initial Catalog=SIGECO_PRUEBAS;User ID=sa;Password=123456"
    Public Connection As New SqlConnection(CadenaConexion)

    Sub Main()

        Dim dt As New DataTable
        dt = RetornaDataTable("EXEC ListaQr")

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                Dim Txt, NombreArch As String

                Txt = dt.Rows(i)(0).ToString() + "|" + dt.Rows(i)(1).ToString() + "|" +
                        dt.Rows(i)(2).ToString() + "|" + dt.Rows(i)(3).ToString() + "|" +
                        dt.Rows(i)(4).ToString() + "|" + Convert.ToDateTime(dt.Rows(i)(5)).ToString("yyyy-MM-dd") + "|" +
                        dt.Rows(i)(6).ToString() + "|" + dt.Rows(i)(7).ToString() + "|"
                NombreArch = dt.Rows(i)(2).ToString() + "-" + dt.Rows(i)(3).ToString()
                GenerarQr(Txt, NombreArch)
            Next
        End If

    End Sub
    Sub GenerarQr(Texto As String, NombreArch As String)
        Dim qrContent As String = Texto

        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)

        Dim qrImage As Bitmap = qrCode.GetGraphic(10) ' 10 es el tamaño del módulo del QR en píxeles

        Dim rutaDestino As String = AppDomain.CurrentDomain.BaseDirectory + "\QR\" & NombreArch & ".jpg"

        ' Guardar el QR en la ruta especificada
        Try
            qrImage.Save(rutaDestino, System.Drawing.Imaging.ImageFormat.Png)
        Catch ex As Exception
        End Try
    End Sub


    Public Function AbrirConexion() As Boolean
        Dim band As Boolean = False
        Try
            Connection.Open()
            band = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return band
    End Function
    Public Function CerrarConexion() As Boolean
        Dim band As Boolean = False
        Try
            Connection.Close()
            band = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return band
    End Function

    Public Function RetornaDataTable(Query As String) As DataTable
        Dim dt As New DataTable
        Try
            If AbrirConexion() Then
                Dim comand As SqlCommand
                Dim sda As New SqlDataAdapter
                comand = New SqlCommand(Query, Connection)
                sda = New SqlDataAdapter(comand)
                sda.Fill(dt)
            End If

        Catch ex As Exception

        Finally
            CerrarConexion()
        End Try
        Return dt
    End Function

End Module
