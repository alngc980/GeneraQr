Imports QRCoder
Imports System.IO
Imports System.Drawing
Imports System.Windows

Module Module1

    Sub Main()




        Console.WriteLine("Ingresa el contenido del QR:")
        Dim qrContent As String = Console.ReadLine()

        ' Generar el QR
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)

        ' Crear el objeto de imagen del QR
        Dim qrImage As Bitmap = qrCode.GetGraphic(10) ' 10 es el tamaño del módulo del QR en píxeles

        ' Ruta de destino para guardar la imagen
        Dim rutaDestino As String = AppDomain.CurrentDomain.BaseDirectory + "\QR\qr.jpg"

        ' Guardar el QR en la ruta especificada
        Try
            qrImage.Save(rutaDestino, System.Drawing.Imaging.ImageFormat.Png)
            Console.WriteLine("QR guardado correctamente en: " & rutaDestino)
        Catch ex As Exception
            Console.WriteLine("Error al guardar el QR: " & ex.Message)
        End Try

        Console.WriteLine("Presiona cualquier tecla para salir.")
        Console.ReadKey()
    End Sub

End Module
