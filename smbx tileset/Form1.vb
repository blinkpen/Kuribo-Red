Imports System.Windows.Forms
Imports System.Drawing
Imports System
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Public Class Form1
    Dim T1 As Integer = 0
    Dim TOPBOX As Integer
    Dim TOPRIGHTBOX As Integer
    Dim LEFTBOX As Integer
    Dim CENTERBOX1 As Integer
    Dim CENTERBOX2 As Integer
    Dim RIGHTBOX1 As Integer
    Dim RIGHTBOX2 As Integer
    Dim BOTTOMLEFTBOX As Integer
    Dim BOTTOMBOX1 As Integer
    Dim BOTTOMBOX2 As Integer
    Dim BOTTOMRIGHTBOX1 As Integer
    Dim BOTTOMRIGHTBOX2 As Integer
    Dim INNER2BOX As Integer
    Dim INNER3BOX As Integer
    Dim INNER4BOX1 As Integer
    Dim INNER4BOX2 As Integer
    Dim TR45BOX As Integer
    Dim BL45BOX As Integer
    Dim BR45BOX1 As Integer
    Dim BR45BOX2 As Integer
    Dim TR26BOX As Integer
    Dim BL26BOX As Integer
    Dim BR26BOX1 As Integer
    Dim BR26BOX2 As Integer
    Dim INNER26BOX2 As Integer
    Dim TR14BOX2 As Integer
    Dim TRINNERBOXB As Integer
    Dim BackgroundImage1 As Bitmap = Nothing
    Dim bmpNew As Bitmap = Nothing
    Dim scaleFactor As Integer = 1
    Dim defaultcurse As Cursor
    Dim down = False
    Dim Brush = Brushes.Black
    Dim COLOR1 As Color = Color.Black
    Dim BMP As Bitmap
    Dim BMP2 As Bitmap
    Dim Draw As Boolean
    Dim PENCIL As Integer = 8
    Private LocalMousePosition As Point


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WebBrowser1.DocumentText = TextBox2.Text
        'My.Settings.INPATH = Environment.CurrentDirectory & "\block"
        'My.Settings.Save()

        ComboBox2.SelectedIndex = 0

        Try
            If My.Settings.INPATH = Nothing Then
                Me.Text = "Kuribo Red"
            Else
                Me.Text = "Kuribo Red: " & My.Settings.INPATH
            End If

            TextBox1.Text = My.Settings.INPATH
            Dim di As New DirectoryInfo(My.Settings.INPATH)
            Dim folder As FileInfo() = di.GetFiles()
            For Each file In folder
                ListBox1.Items.Add(file)
            Next

        Catch ex As Exception

        End Try




        FolderBrowserDialog1.SelectedPath = My.Settings.INPATH
        ComboBox1.SelectedIndex = 0
        ColorDialog1.Color = My.Settings.panelcolor
        PictureBox1.BackColor = My.Settings.panelcolor
        Panel1.BackColor = My.Settings.panelcolor
        Panel2.BackColor = My.Settings.panelcolor
        Panel3.BackColor = My.Settings.panelcolor
        Panel4.BackColor = My.Settings.panelcolor
        Panel5.BackColor = My.Settings.panelcolor
        Panel6.BackColor = My.Settings.panelcolor
        Panel7.BackColor = My.Settings.panelcolor
        TOPBOX = top.Location.X
        TOPRIGHTBOX = topright.Location.X
        LEFTBOX = left.Location.Y
        CENTERBOX1 = center.Location.X
        CENTERBOX2 = center.Location.Y
        RIGHTBOX1 = right.Location.X
        RIGHTBOX2 = right.Location.Y
        BOTTOMLEFTBOX = bottomleft.Location.Y
        BOTTOMBOX1 = bottom.Location.X
        BOTTOMBOX2 = bottom.Location.Y
        BOTTOMRIGHTBOX1 = bottomright.Location.X
        BOTTOMRIGHTBOX2 = bottomright.Location.Y
        INNER2BOX = inner2.Location.X
        INNER3BOX = inner3.Location.Y
        INNER4BOX1 = inner4.Location.X
        INNER4BOX2 = inner4.Location.Y
        TR45BOX = TR45.Location.X
        BL45BOX = BL45.Location.Y
        BR45BOX1 = BR45.Location.X
        BR45BOX2 = BR45.Location.Y
        TR26BOX = TR26.Location.X
        BL26BOX = BL26.Location.Y
        BR26BOX1 = BR26.Location.X
        BR26BOX2 = BR26.Location.Y
        INNER26BOX2 = Inner26B.Location.X
        TR14BOX2 = TR14.Location.X
        TRINNERBOXB = TRINNER14.Location.X


        canvaseditor.SizeMode = PictureBoxSizeMode.StretchImage

        BMP = New Bitmap(canvaseditor.Width, canvaseditor.Height)
        canvaseditor.Image = BMP

        ' SEND CLICK EVENT INITIALLY
        tiledcanvas.BackgroundImage = topleft.Image
        Label1.Text = "Top-Left"
        ComboBox2.SelectedItem = "Top-Left"
        canvaseditor.Focus()
        Dim CANX As Integer = canvaseditor.Width - 1
        Dim CANY As Integer = canvaseditor.Height - 1
        Label5.Text = CANX / 8 & "x" & CANY / 8 & " pixels"
    End Sub





    ' Flood fill the point.
    Public Sub SafeFloodFill(ByVal bm As Bitmap, ByVal x As Integer, ByVal y As Integer, ByVal new_color As Color)
        ' Get the old and new colors.
        Dim old_color As Color = bm.GetPixel(x, y)

        ' The following "If Then" test was added by Reuben
        ' Jollif
        ' to protect the code in case the start pixel
        ' has the same color as the fill color.
        If old_color.ToArgb <> new_color.ToArgb Then
            ' Start with the original point in the stack.
            Dim pts As New Stack(1000)
            pts.Push(New Point(x, y))
            bm.SetPixel(x, y, new_color)

            ' While the stack is not empty, process a point.
            Do While pts.Count > 0
                Dim pt As Point = DirectCast(pts.Pop(), Point)
                If pt.X > 0 Then SafeCheckPoint(bm, pts, pt.X - _
                    1, pt.Y, old_color, new_color)
                If pt.Y > 0 Then SafeCheckPoint(bm, pts, pt.X, _
                    pt.Y - 1, old_color, new_color)
                If pt.X < bm.Width - 1 Then SafeCheckPoint(bm, _
                    pts, pt.X + 1, pt.Y, old_color, new_color)
                If pt.Y < bm.Height - 1 Then SafeCheckPoint(bm, _
                    pts, pt.X, pt.Y + 1, old_color, new_color)
            Loop
        End If
    End Sub

    ' See if this point should be added to the stack.
    Private Sub SafeCheckPoint(ByVal bm As Bitmap, ByVal pts As Stack, ByVal x As Integer, ByVal y As Integer, ByVal old_color As Color, ByVal new_color As Color)
        Dim clr As Color = bm.GetPixel(x, y)
        If clr.Equals(old_color) Then
            pts.Push(New Point(x, y))
            bm.SetPixel(x, y, new_color)
        End If
    End Sub
















    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()
        FolderBrowserDialog1.ShowDialog()
        My.Settings.INPATH = FolderBrowserDialog1.SelectedPath
        My.Settings.Save()

        Try
            Me.Text = Me.Text & ": " & FolderBrowserDialog1.SelectedPath
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            Dim di As New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
            Dim folder As FileInfo() = di.GetFiles()
            For Each file In folder
                ListBox1.Items.Add(file)
            Next
        Catch ex As Exception

        End Try

        Try
            tiledcanvas.BackgroundImage = Nothing
            topleft.Image = Nothing
            top.Image = Nothing
            topright.Image = Nothing
            left.Image = Nothing
            center.Image = Nothing
            right.Image = Nothing
            bottomleft.Image = Nothing
            bottom.Image = Nothing
            bottomright.Image = Nothing
            inner1.Image = Nothing
            inner2.Image = Nothing
            inner3.Image = Nothing
            inner4.Image = Nothing
            innerA.Image = Nothing
            innerB.Image = Nothing
            innerC.Image = Nothing
            innerD.Image = Nothing
            TL45.Image = Nothing
            TR45.Image = Nothing
            BL45.Image = Nothing
            BR45.Image = Nothing
            SLOPE45A.Image = Nothing
            SLOPE45B.Image = Nothing
            SLOPE45C.Image = Nothing
            SLOPE45D.Image = Nothing
            TL26.Image = Nothing
            TR26.Image = Nothing
            BL26.Image = Nothing
            BR26.Image = Nothing
            SLOPE26A.Image = Nothing
            SLOPE26B.Image = Nothing
            SLOPE26C.Image = Nothing
            SLOPE26D.Image = Nothing
            Inner26A.Image = Nothing
            Inner26B.Image = Nothing
            TL14.Image = Nothing
            TR14.Image = Nothing
            TLINNER14.Image = Nothing
            TRINNER14.Image = Nothing
            SLOPE14A.Image = Nothing
            SLOPE14B.Image = Nothing
            SLOPE14C.Image = Nothing
            SLOPE14D.Image = Nothing

            If ComboBox1.SelectedItem = "SMW-Grass" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-80.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-81.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-82.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-83.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-87.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-84.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-265.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-264.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-266.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-273.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-263.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-85.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-86.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-299.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-300.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-309.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-310.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-616.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-617.gif")
                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-619.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-618.gif")
            End If

            If ComboBox1.SelectedItem = "SMW-Cave" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-246.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-250.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-247.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-252.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-251.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-253.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-248.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-254.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-249.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-256.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-255.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-257.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-258.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-316.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-315.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-317.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-318.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-365.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-366.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-368.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-367.gif")

                TL14.Image = Image.FromFile(My.Settings.INPATH & "\block-321.gif")
                TR14.Image = Image.FromFile(My.Settings.INPATH & "\block-319.gif")
                TLINNER14.Image = Image.FromFile(My.Settings.INPATH & "\block-320.gif")
                TRINNER14.Image = Image.FromFile(My.Settings.INPATH & "\block-322.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Wood" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-7.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-3.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-6.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-15.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-16.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-17.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-274.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-276.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-275.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-600.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-601.gif")

                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-602.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-603.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-604.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-605.gif")
                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-607.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-606.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Grass" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-9.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-10.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-11.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-18.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-19.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-20.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-279.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-278.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-277.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-305.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-307.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-311.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-313.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-306.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-308.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-312.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-314.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Cave" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-344.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-345.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-346.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-347.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-348.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-349.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-350.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-351.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-352.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-353.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-354.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-355.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-356.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-358.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-359.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-362.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-363.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-357.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-360.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-361.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-364.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Desert" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-162.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-163.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-164.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-165.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-166.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-167.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-286.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-285.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-284.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-635.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-637.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-636.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-638.gif")
            End If

            If ComboBox1.SelectedItem = "SMW-Castle" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-425.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-424.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-426.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-423.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-422.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-421.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-427.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-419.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-436.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-418.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-417.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-416.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-415.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-452.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-451.gif")

                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-449.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-450.gif")
            End If

        Catch ex As Exception

        End Try



    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            tiledcanvas.BackgroundImage = Nothing
            topleft.Image = Nothing
            top.Image = Nothing
            topright.Image = Nothing
            left.Image = Nothing
            center.Image = Nothing
            right.Image = Nothing
            bottomleft.Image = Nothing
            bottom.Image = Nothing
            bottomright.Image = Nothing
            inner1.Image = Nothing
            inner2.Image = Nothing
            inner3.Image = Nothing
            inner4.Image = Nothing
            innerA.Image = Nothing
            innerB.Image = Nothing
            innerC.Image = Nothing
            innerD.Image = Nothing
            TL45.Image = Nothing
            TR45.Image = Nothing
            BL45.Image = Nothing
            BR45.Image = Nothing
            SLOPE45A.Image = Nothing
            SLOPE45B.Image = Nothing
            SLOPE45C.Image = Nothing
            SLOPE45D.Image = Nothing
            TL26.Image = Nothing
            TR26.Image = Nothing
            BL26.Image = Nothing
            BR26.Image = Nothing
            SLOPE26A.Image = Nothing
            SLOPE26B.Image = Nothing
            SLOPE26C.Image = Nothing
            SLOPE26D.Image = Nothing
            Inner26A.Image = Nothing
            Inner26B.Image = Nothing
            TL14.Image = Nothing
            TR14.Image = Nothing
            TLINNER14.Image = Nothing
            TRINNER14.Image = Nothing
            SLOPE14A.Image = Nothing
            SLOPE14B.Image = Nothing
            SLOPE14C.Image = Nothing
            SLOPE14D.Image = Nothing

            If ComboBox1.SelectedItem = "SMW-Grass" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-80.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-81.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-82.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-83.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-87.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-84.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-265.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-264.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-266.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-273.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-263.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-85.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-86.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-299.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-300.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-309.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-310.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-616.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-617.gif")
                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-619.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-618.gif")

            ElseIf ComboBox1.SelectedItem = "SMW-Cave" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-246.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-250.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-247.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-252.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-251.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-253.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-248.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-254.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-249.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-256.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-255.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-257.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-258.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-316.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-315.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-317.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-318.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-365.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-366.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-368.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-367.gif")

                TL14.Image = Image.FromFile(My.Settings.INPATH & "\block-321.gif")
                TR14.Image = Image.FromFile(My.Settings.INPATH & "\block-319.gif")
                TLINNER14.Image = Image.FromFile(My.Settings.INPATH & "\block-320.gif")
                TRINNER14.Image = Image.FromFile(My.Settings.INPATH & "\block-322.gif")

            ElseIf ComboBox1.SelectedItem = "SMB3-Wood" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-7.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-3.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-6.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-15.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-16.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-17.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-274.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-276.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-275.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-600.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-601.gif")

                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-602.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-603.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-604.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-605.gif")
                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-607.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-606.gif")

            ElseIf ComboBox1.SelectedItem = "SMB3-Grass" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-9.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-10.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-11.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-18.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-19.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-20.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-279.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-278.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-277.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-305.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-307.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-311.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-313.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-306.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-308.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-312.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-314.gif")

            ElseIf ComboBox1.SelectedItem = "SMB3-Cave" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-344.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-345.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-346.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-347.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-348.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-349.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-350.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-351.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-352.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-353.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-354.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-355.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-356.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-358.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-359.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-362.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-363.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-357.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-360.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-361.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-364.gif")

            ElseIf ComboBox1.SelectedItem = "SMB3-Desert" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-162.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-163.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-164.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-165.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-166.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-167.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-286.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-285.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-284.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-635.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-637.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-636.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-638.gif")

            ElseIf ComboBox1.SelectedItem = "SMW-Castle" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-425.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-424.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-426.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-423.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-422.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-421.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-427.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-419.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-436.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-418.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-417.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-416.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-415.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-452.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-451.gif")

                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-449.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-450.gif")
            End If

        Catch ex As Exception

        End Try

        ' SEND CLICK EVENT INITIALLY
        If Not canvaseditor.Image Is Nothing Then
            canvaseditor.Image.Dispose()
        End If
        canvaseditor.Image = center.Image

        tiledcanvas.BackgroundImage = topleft.Image
        Label1.Text = "Top-Left"
        ComboBox2.SelectedItem = "Top-Left"
        canvaseditor.Image = topleft.Image
        canvaseditor.Focus()
        Dim CANX As Integer = canvaseditor.Width - 1
        Dim CANY As Integer = canvaseditor.Height - 1
        Label5.Text = CANX / 8 & "x" & CANY / 8 & " pixels"
    End Sub

    Private Sub topleft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles topleft.Click
        tiledcanvas.BackgroundImage = topleft.Image
        Label1.Text = "Top-Left"
        ComboBox2.SelectedItem = "Top-Left"

    End Sub

    Private Sub top_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles top.Click
        tiledcanvas.BackgroundImage = top.Image
        Label1.Text = "Top"
        ComboBox2.SelectedItem = "Top"

    End Sub

    Private Sub topright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles topright.Click
        tiledcanvas.BackgroundImage = topright.Image
        Label1.Text = "Top-Right"
        ComboBox2.SelectedItem = "Top-Right"

    End Sub

    Private Sub left_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles left.Click
        tiledcanvas.BackgroundImage = left.Image
        Label1.Text = "Left"
        ComboBox2.SelectedItem = "Left"

    End Sub

    Private Sub center_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles center.Click
        tiledcanvas.BackgroundImage = center.Image
        Label1.Text = "Center"
        ComboBox2.SelectedItem = "Center"

    End Sub

    Private Sub right_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles right.Click
        tiledcanvas.BackgroundImage = right.Image
        Label1.Text = "Right"
        ComboBox2.SelectedItem = "Right"

    End Sub

    Private Sub bottomleft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bottomleft.Click
        tiledcanvas.BackgroundImage = bottomleft.Image
        Label1.Text = "Bottom-Left"
        ComboBox2.SelectedItem = "Bottom-Left"

    End Sub

    Private Sub bottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bottom.Click
        tiledcanvas.BackgroundImage = bottom.Image
        Label1.Text = "Bottom"
        ComboBox2.SelectedItem = "Bottom"

    End Sub

    Private Sub bottomright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bottomright.Click
        tiledcanvas.BackgroundImage = bottomright.Image
        Label1.Text = "Bottom-Right"
        ComboBox2.SelectedItem = "Bottom-Right"

    End Sub


    Private Sub inner1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles inner1.Click
        innerA.Image = Nothing
        innerB.Image = Nothing
        innerC.Image = Nothing
        innerD.Image = Nothing

        innerA.Image = inner1.Image
        innerB.Image = bottom.Image
        innerC.Image = right.Image

        Label1.Text = "Top-Left-Inner"
        ComboBox2.SelectedItem = "Top-Left Inner"

    End Sub

    Private Sub inner2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles inner2.Click
        innerA.Image = Nothing
        innerB.Image = Nothing
        innerC.Image = Nothing
        innerD.Image = Nothing

        innerA.Image = bottom.Image
        innerB.Image = inner2.Image
        innerD.Image = left.Image

        Label1.Text = "Top-Right-Inner"
        ComboBox2.SelectedItem = "Top-Right Inner"

    End Sub

    Private Sub inner3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles inner3.Click
        innerA.Image = Nothing
        innerB.Image = Nothing
        innerC.Image = Nothing
        innerD.Image = Nothing

        innerA.Image = right.Image
        innerC.Image = inner3.Image
        innerD.Image = top.Image

        Label1.Text = "Bottom-Left-Inner"
        ComboBox2.SelectedItem = "Bottom-Left Inner"

    End Sub

    Private Sub inner4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles inner4.Click
        innerA.Image = Nothing
        innerB.Image = Nothing
        innerC.Image = Nothing
        innerD.Image = Nothing

        innerB.Image = left.Image
        innerC.Image = top.Image
        innerD.Image = inner4.Image

        Label1.Text = "Bottom-Right-Inner"
        ComboBox2.SelectedItem = "Bottom-Right Inner"

    End Sub

    Private Sub TL45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TL45.Click
        SLOPE45A.Image = Nothing
        SLOPE45B.Image = Nothing
        SLOPE45C.Image = Nothing
        SLOPE45D.Image = Nothing

        SLOPE45B.Image = TL45.Image
        SLOPE45C.Image = TL45.Image
        SLOPE45D.Image = inner4.Image

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            SLOPE45D.Image = center.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Desert" Then
            SLOPE45D.Image = center.Image
        End If

        If ComboBox1.SelectedItem = "SMW-Castle" Then
            SLOPE45D.Image = Inner26B.Image
        End If


        Label1.Text = "45°-Top-Left"

        ComboBox2.SelectedItem = "45 Slope A"

    End Sub

    Private Sub TR45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TR45.Click
        SLOPE45A.Image = Nothing
        SLOPE45B.Image = Nothing
        SLOPE45C.Image = Nothing
        SLOPE45D.Image = Nothing


        SLOPE45A.Image = TR45.Image
        SLOPE45C.Image = inner3.Image
        SLOPE45D.Image = TR45.Image

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            SLOPE45C.Image = center.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Desert" Then
            SLOPE45C.Image = center.Image
        End If

        If ComboBox1.SelectedItem = "SMW-Castle" Then
            SLOPE45C.Image = Inner26A.Image
        End If


        Label1.Text = "45°-Top-Right"

        ComboBox2.SelectedItem = "45 Slope B"

    End Sub

    Private Sub BL45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BL45.Click
        SLOPE45A.Image = Nothing
        SLOPE45B.Image = Nothing
        SLOPE45C.Image = Nothing
        SLOPE45D.Image = Nothing


        SLOPE45A.Image = BL45.Image
        SLOPE45B.Image = inner2.Image
        SLOPE45D.Image = BL45.Image

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            SLOPE45B.Image = center.Image
        End If


        Label1.Text = "45°-Bottom-Left"
        ComboBox2.SelectedItem = "45 Slope C"
    End Sub

    Private Sub BR45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BR45.Click
        SLOPE45A.Image = Nothing
        SLOPE45B.Image = Nothing
        SLOPE45C.Image = Nothing
        SLOPE45D.Image = Nothing


        SLOPE45A.Image = inner1.Image
        SLOPE45B.Image = BR45.Image
        SLOPE45C.Image = BR45.Image

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            SLOPE45A.Image = center.Image
        End If


        Label1.Text = "45°-Bottom-Right"

        ComboBox2.SelectedItem = "45 Slope D"
    End Sub

    Private Sub TL26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TL26.Click
        SLOPE26A.Image = Nothing
        SLOPE26B.Image = Nothing
        SLOPE26C.Image = Nothing
        SLOPE26D.Image = Nothing

        If ComboBox1.SelectedItem = "SMW-Cave" Then
            Dim image1 As New Bitmap(inner4.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26D.Image = Image3
            SLOPE26B.Image = TL26.Image
            SLOPE26C.Image = TL26.Image
        End If

        If ComboBox1.SelectedItem = "SMW-Grass" Then
            Dim image1 As New Bitmap(Inner26B.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26D.Image = Image3
            SLOPE26B.Image = TL26.Image
            SLOPE26C.Image = TL26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Wood" Then
            Dim image1 As New Bitmap(Inner26B.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26D.Image = Image3
            SLOPE26B.Image = TL26.Image
            SLOPE26C.Image = TL26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26D.Image = Image3
            SLOPE26B.Image = TL26.Image
            SLOPE26C.Image = TL26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Desert" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26D.Image = Image3
            SLOPE26B.Image = TL26.Image
            SLOPE26C.Image = TL26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Cave" Then
            Dim image1 As New Bitmap(inner4.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26D.Image = Image3
            SLOPE26B.Image = TL26.Image
            SLOPE26C.Image = TL26.Image
        End If

        ComboBox2.SelectedItem = "26 Slope A"
    End Sub

    Private Sub TR26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TR26.Click
        SLOPE26A.Image = Nothing
        SLOPE26B.Image = Nothing
        SLOPE26C.Image = Nothing
        SLOPE26D.Image = Nothing

        If ComboBox1.SelectedItem = "SMW-Cave" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(inner3.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26C.Image = Image3
            SLOPE26A.Image = TR26.Image
            SLOPE26D.Image = TR26.Image
        End If

        If ComboBox1.SelectedItem = "SMW-Grass" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(Inner26A.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26C.Image = Image3
            SLOPE26A.Image = TR26.Image
            SLOPE26D.Image = TR26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Wood" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(Inner26A.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26C.Image = Image3
            SLOPE26A.Image = TR26.Image
            SLOPE26D.Image = TR26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26C.Image = Image3
            SLOPE26A.Image = TR26.Image
            SLOPE26D.Image = TR26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Desert" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26C.Image = Image3
            SLOPE26A.Image = TR26.Image
            SLOPE26D.Image = TR26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Cave" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(inner3.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26C.Image = Image3
            SLOPE26A.Image = TR26.Image
            SLOPE26D.Image = TR26.Image
        End If

        ComboBox2.SelectedItem = "26 Slope B"
    End Sub

    Private Sub BL26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BL26.Click
        SLOPE26A.Image = Nothing
        SLOPE26B.Image = Nothing
        SLOPE26C.Image = Nothing
        SLOPE26D.Image = Nothing

        If ComboBox1.SelectedItem = "SMW-Cave" Then
            Dim image1 As New Bitmap(inner2.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26B.Image = Image3
            SLOPE26A.Image = BL26.Image
            SLOPE26D.Image = BL26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26B.Image = Image3
            SLOPE26A.Image = BL26.Image
            SLOPE26D.Image = BL26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Cave" Then
            Dim image1 As New Bitmap(inner2.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26B.Image = Image3
            SLOPE26A.Image = BL26.Image
            SLOPE26D.Image = BL26.Image
        End If

        ComboBox2.SelectedItem = "26 Slope C"
    End Sub

    Private Sub BR26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BR26.Click
        SLOPE26A.Image = Nothing
        SLOPE26B.Image = Nothing
        SLOPE26C.Image = Nothing
        SLOPE26D.Image = Nothing

        If ComboBox1.SelectedItem = "SMW-Cave" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(inner1.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26A.Image = Image3
            SLOPE26B.Image = BR26.Image
            SLOPE26C.Image = BR26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(center.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26A.Image = Image3
            SLOPE26B.Image = BR26.Image
            SLOPE26C.Image = BR26.Image
        End If

        If ComboBox1.SelectedItem = "SMB3-Cave" Then
            Dim image1 As New Bitmap(center.Image)
            Dim image2 As New Bitmap(inner1.Image)
            Dim Image3 As New Bitmap(64, 32)
            Dim g As Graphics = Graphics.FromImage(Image3)
            g.DrawImage(image1, New Point(0, 0))
            g.DrawImage(image2, New Point(32, 0))
            g.Dispose()
            g = Nothing
            SLOPE26A.Image = Image3
            SLOPE26B.Image = BR26.Image
            SLOPE26C.Image = BR26.Image
        End If

        ComboBox2.SelectedItem = "26 Slope D"
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            My.Computer.Audio.Play(My.Resources.smw_switch_activated, AudioPlayMode.Background)
            Panel1.Visible = True
            Panel2.Visible = True
            Panel3.Visible = True
            Panel4.Visible = True
            Panel5.Visible = True
            Panel6.Visible = True
            Panel7.Visible = True
            TOPBOX = TOPBOX + 2
            TOPRIGHTBOX = TOPRIGHTBOX + 4
            LEFTBOX = LEFTBOX + 2
            CENTERBOX1 = CENTERBOX1 + 2
            CENTERBOX2 = CENTERBOX2 + 2
            RIGHTBOX1 = RIGHTBOX1 + 4
            RIGHTBOX2 = RIGHTBOX2 + 2
            BOTTOMLEFTBOX = BOTTOMLEFTBOX + 4
            BOTTOMBOX1 = BOTTOMBOX1 + 2
            BOTTOMBOX2 = BOTTOMBOX2 + 4
            BOTTOMRIGHTBOX1 = BOTTOMRIGHTBOX1 + 4
            BOTTOMRIGHTBOX2 = BOTTOMRIGHTBOX2 + 4
            INNER2BOX = INNER2BOX + 2
            INNER3BOX = INNER3BOX + 2
            INNER4BOX1 = INNER4BOX1 + 2
            INNER4BOX2 = INNER4BOX2 + 2
            TR45BOX = TR45BOX + 2
            BL45BOX = BL45BOX + 2
            BR45BOX1 = BR45BOX1 + 2
            BR45BOX2 = BR45BOX2 + 2
            TR26BOX = TR26BOX + 2
            BL26BOX = BL26BOX + 2
            BR26BOX1 = BR26BOX1 + 2
            BR26BOX2 = BR26BOX2 + 2
            INNER26BOX2 = INNER26BOX2 + 2
            TR14BOX2 = TR14BOX2 + 2
            TRINNERBOXB = TRINNERBOXB + 2
            CheckBox2.Text = "On"
        Else
            My.Computer.Audio.Play(My.Resources.smw_stomp_koopa_kid, AudioPlayMode.Background)
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            TOPBOX = TOPBOX - 2
            TOPRIGHTBOX = TOPRIGHTBOX - 4
            LEFTBOX = LEFTBOX - 2
            CENTERBOX1 = CENTERBOX1 - 2
            CENTERBOX2 = CENTERBOX2 - 2
            RIGHTBOX1 = RIGHTBOX1 - 4
            RIGHTBOX2 = RIGHTBOX2 - 2
            BOTTOMLEFTBOX = BOTTOMLEFTBOX - 4
            BOTTOMBOX1 = BOTTOMBOX1 - 2
            BOTTOMBOX2 = BOTTOMBOX2 - 4
            BOTTOMRIGHTBOX1 = BOTTOMRIGHTBOX1 - 4
            BOTTOMRIGHTBOX2 = BOTTOMRIGHTBOX2 - 4
            INNER2BOX = INNER2BOX - 2
            INNER3BOX = INNER3BOX - 2
            INNER4BOX1 = INNER4BOX1 - 2
            INNER4BOX2 = INNER4BOX2 - 2
            TR45BOX = TR45BOX - 2
            BL45BOX = BL45BOX - 2
            BR45BOX1 = BR45BOX1 - 2
            BR45BOX2 = BR45BOX2 - 2
            TR26BOX = TR26BOX - 2
            BL26BOX = BL26BOX - 2
            BR26BOX1 = BR26BOX1 - 2
            BR26BOX2 = BR26BOX2 - 2
            INNER26BOX2 = INNER26BOX2 - 2
            TR14BOX2 = TR14BOX2 - 2
            TRINNERBOXB = TRINNERBOXB - 2
            CheckBox2.Text = "Off"
        End If
        top.Left = TOPBOX
        topright.Left = TOPRIGHTBOX
        left.Top = LEFTBOX
        center.Left = CENTERBOX1
        center.Top = CENTERBOX2
        right.Left = RIGHTBOX1
        right.Top = RIGHTBOX2
        bottomleft.Top = BOTTOMLEFTBOX
        bottom.Left = BOTTOMBOX1
        bottom.Top = BOTTOMBOX2
        bottomright.Left = BOTTOMRIGHTBOX1
        bottomright.Top = BOTTOMRIGHTBOX2
        inner2.Left = INNER2BOX
        inner3.Top = INNER3BOX
        inner4.Left = INNER4BOX1
        inner4.Top = INNER4BOX2
        TR45.Left = TR45BOX
        BL45.Top = BL45BOX
        BR45.Left = BR45BOX1
        BR45.Top = BR45BOX2
        TR26.Left = TR26BOX
        BL26.Top = BL26BOX
        BR26.Left = BR26BOX1
        BR26.Top = BR26BOX2
        Inner26B.Left = INNER26BOX2
        TR14.Left = TR14BOX2
        TRINNER14.Left = TRINNERBOXB




    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        ColorDialog1.ShowDialog()
        My.Settings.panelcolor = ColorDialog1.Color
        My.Settings.Save()
        PictureBox1.BackColor = My.Settings.panelcolor
        Panel1.BackColor = My.Settings.panelcolor
        Panel2.BackColor = My.Settings.panelcolor
        Panel3.BackColor = My.Settings.panelcolor
        Panel4.BackColor = My.Settings.panelcolor
        Panel5.BackColor = My.Settings.panelcolor
        Panel6.BackColor = My.Settings.panelcolor
        Panel7.BackColor = My.Settings.panelcolor

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        T1 = T1 + 1
        If T1 = 1 Then
            Me.Icon = My.Resources.k1
        ElseIf T1 = 2 Then
            Me.Icon = My.Resources.k2
            T1 = 0
        End If

       

    End Sub


    Private Sub TL14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TL14.Click
        SLOPE14A.Image = Nothing
        SLOPE14B.Image = Nothing
        SLOPE14C.Image = Nothing
        SLOPE14D.Image = Nothing

        SLOPE14B.Image = TL14.Image
        SLOPE14C.Image = TL14.Image
        SLOPE14D.Image = TRINNER14.Image

        'CANVAS PIXEL GRID CODE
        If TL14.Image Is Nothing Then
        Else
            canvaseditor.Image = Nothing
            canvaseditor.Image = TL14.Image
            canvaseditor.Width = TL14.Width * 8 + 1
            canvaseditor.Height = TL14.Height * 8 + 1
            'load and draw the image(s) once
            BackgroundImage1 = New Bitmap(TL14.Image)
            bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
            Using g As Graphics = Graphics.FromImage(bmpNew)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
            End Using
            canvaseditor.Focus()
            GroupBox13.Focus()
            ComboBox2.SelectedItem = "14 Slope A"
            Me.CenterToScreen()
        End If




    End Sub


    Private Sub TR14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TR14.Click
        SLOPE14A.Image = Nothing
        SLOPE14B.Image = Nothing
        SLOPE14C.Image = Nothing
        SLOPE14D.Image = Nothing

        SLOPE14A.Image = TR14.Image
        SLOPE14D.Image = TR14.Image
        SLOPE14C.Image = TLINNER14.Image

        'CANVAS PIXEL GRID CODE
        If TR14.Image Is Nothing Then
        Else
            canvaseditor.Image = Nothing
            canvaseditor.Image = TR14.Image
            canvaseditor.Width = TR14.Width * 8 + 1
            canvaseditor.Height = TR14.Height * 8 + 1
            'load and draw the image(s) once
            BackgroundImage1 = New Bitmap(TR14.Image)
            bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
            Using g As Graphics = Graphics.FromImage(bmpNew)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
            End Using
            canvaseditor.Focus()
            GroupBox13.Focus()
            ComboBox2.SelectedItem = "14 Slope B"
            Me.CenterToScreen()
        End If

    End Sub

    Private Sub canvaseditor_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles canvaseditor.GotFocus
        Dim CANX As Integer = canvaseditor.Width - 1
        Dim CANY As Integer = canvaseditor.Height - 1
        Label5.Text = CANX / 8 & "x" & CANY / 8 & " pixels"
        Panel11.Width = canvaseditor.Width + 4
        Panel11.Height = canvaseditor.Height + 4
    End Sub


    Private Sub canvaseditor_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles canvaseditor.Paint
        If Not bmpNew Is Nothing Then
            e.Graphics.DrawImage(bmpNew, 0, 0)
        End If

        If CheckBox1.Checked = True Then
            Dim g As Graphics = e.Graphics
            Dim pn As New Pen(Color.DimGray) '~~~ color of the lines

            Dim x As Integer
            Dim y As Integer

            Dim intSpacing As Integer = 8  '~~~ spacing between adjacent lines

            '~~~ Draw the horizontal lines
            x = canvaseditor.Width
            For y = 0 To canvaseditor.Height Step intSpacing
                g.DrawLine(pn, New Point(0, y), New Point(x, y))
            Next

            '~~~ Draw the vertical lines
            y = canvaseditor.Height
            For x = 0 To canvaseditor.Width Step intSpacing
                g.DrawLine(pn, New Point(x, 0), New Point(x, y))
            Next
        Else

        End If

    End Sub


    Private Sub canvaseditor_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles canvaseditor.MouseEnter
        Cursor = defaultcurse
        
    End Sub

    Private Sub canvaseditor_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles canvaseditor.MouseLeave
        Cursor = Cursors.Arrow
        Label6.Text = "Ready"
    End Sub

    Private Sub canvaseditor_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles canvaseditor.MouseHover
        'Cursor = defaultcurse
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If RadioButton1.Checked = True Then
            Dim cur As Icon
            cur = My.Resources._1994
            'Cursor = New Cursor(cur.Handle)
            defaultcurse = New Cursor(cur.Handle)
        ElseIf RadioButton2.Checked = True Then
            Dim cur As Icon
            cur = My.Resources.paint_buket_1994
            'Cursor = New Cursor(cur.Handle)
            defaultcurse = New Cursor(cur.Handle)
        ElseIf RadioButton3.Checked = True Then
            Dim cur As Icon
            cur = My.Resources.eyedroppa94
            'Cursor = New Cursor(cur.Handle)
            defaultcurse = New Cursor(cur.Handle)
        End If
        Label9.Text = My.Settings.CURRENTSELECTION
    End Sub


    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox3.Text = "On"
            Panel11.BackColor = Color.Red
        Else
            CheckBox3.Text = "Off"
            Panel11.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub canvaseditor_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvaseditor.MouseMove
        Dim x As Integer
        Dim y As Integer
        x = (e.X \ 8) + 1
        y = (e.Y \ 8) + 1


        Label6.Text = "X:" & x & ", Y:" & y





        If RadioButton1.Checked = True And PENCIL = 16 Then
          
        End If









        If RadioButton3.Checked = True Then
            Dim Image1 As New Bitmap(canvaseditor.Image, canvaseditor.Width, canvaseditor.Height)

            Using g As Graphics = Graphics.FromImage(Image1)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                g.DrawImage(BackgroundImage1, 0, 0, Image1.Width, Image1.Height)
            End Using

            Try
                COLOR1 = Image1.GetPixel(e.X, e.Y)
                ColorDialog2.Color = COLOR1
                PictureBox3.BackColor = COLOR1
            Catch ex As Exception

            End Try


        ElseIf RadioButton1.Checked = True Then
            If Draw = True Then
                PaintBrush(e.X, e.Y)
                Dim from_bm As New Bitmap(BMP)

                ' Make the destination Bitmap.
                Dim wid As Integer = BMP.Width \ 8
                Dim hgt As Integer = BMP.Height \ 8
                Dim to_bm As New Bitmap(wid, hgt)

                ' Copy the image.
                Dim gr As Graphics = Graphics.FromImage(to_bm)
                gr.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                gr.DrawImage(from_bm, 0, 0, wid, hgt)


                If ComboBox2.SelectedItem = "Top-Left" Then
                    topleft.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Top" Then
                    top.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Top-Right" Then
                    topright.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Left" Then
                    left.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Center" Then
                    center.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Right" Then
                    right.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Bottom-Left" Then
                    bottomleft.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Bottom" Then
                    bottom.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Bottom-Right" Then
                    bottomright.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Top-Left Inner" Then
                    inner1.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Top-Right Inner" Then
                    inner2.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Bottom-Left Inner" Then
                    inner3.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "Bottom-Right Inner" Then
                    inner4.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "45 Slope A" Then
                    TL45.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "45 Slope B" Then
                    TR45.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "45 Slope C" Then
                    BL45.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "45 Slope D" Then
                    BR45.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "26 Slope A" Then
                    TL26.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "26 Slope B" Then
                    TR26.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "26 Slope C" Then
                    BL26.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "26 Slope D" Then
                    BR26.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "26 Inner 1" Then
                    Inner26A.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "26 Inner 2" Then
                    Inner26B.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "14 Slope A" Then
                    TL14.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "14 Slope B" Then
                    TR14.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "14 Inner 1" Then
                    TLINNER14.Image = to_bm
                ElseIf ComboBox2.SelectedItem = "14 Inner 2" Then
                    TRINNER14.Image = to_bm
                End If


               


                If ComboBox2.SelectedItem = "Top-Left" Then
                    'CANVAS PIXEL GRID CODE
                    If topleft.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = topleft.Image
                        canvaseditor.Width = topleft.Width * 8 + 1
                        canvaseditor.Height = topleft.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(topleft.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top" Then
                    'CANVAS PIXEL GRID CODE
                    If top.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = top.Image
                        canvaseditor.Width = top.Width * 8 + 1
                        canvaseditor.Height = top.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(top.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top-Right" Then
                    'CANVAS PIXEL GRID CODE
                    If topright.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = topright.Image
                        canvaseditor.Width = topright.Width * 8 + 1
                        canvaseditor.Height = topright.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(topright.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Left" Then
                    'CANVAS PIXEL GRID CODE
                    If left.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = left.Image
                        canvaseditor.Width = left.Width * 8 + 1
                        canvaseditor.Height = left.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(left.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Center" Then
                    'CANVAS PIXEL GRID CODE
                    If center.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = center.Image
                        canvaseditor.Width = center.Width * 8 + 1
                        canvaseditor.Height = center.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(center.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Right" Then
                    'CANVAS PIXEL GRID CODE
                    If right.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = right.Image
                        canvaseditor.Width = right.Width * 8 + 1
                        canvaseditor.Height = right.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(right.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Left" Then
                    'CANVAS PIXEL GRID CODE
                    If bottomleft.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = bottomleft.Image
                        canvaseditor.Width = bottomleft.Width * 8 + 1
                        canvaseditor.Height = bottomleft.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(bottomleft.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom" Then
                    'CANVAS PIXEL GRID CODE
                    If bottom.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = bottom.Image
                        canvaseditor.Width = bottom.Width * 8 + 1
                        canvaseditor.Height = bottom.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(bottom.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Right" Then
                    'CANVAS PIXEL GRID CODE
                    If bottomright.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = bottomright.Image
                        canvaseditor.Width = bottomright.Width * 8 + 1
                        canvaseditor.Height = bottomright.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(bottomright.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top-Left Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner1.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner1.Image
                        canvaseditor.Width = inner1.Width * 8 + 1
                        canvaseditor.Height = inner1.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner1.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top-Right Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner2.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner2.Image
                        canvaseditor.Width = inner2.Width * 8 + 1
                        canvaseditor.Height = inner2.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner2.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Left Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner3.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner3.Image
                        canvaseditor.Width = inner3.Width * 8 + 1
                        canvaseditor.Height = inner3.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner3.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Right Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner4.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner4.Image
                        canvaseditor.Width = inner4.Width * 8 + 1
                        canvaseditor.Height = inner4.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner4.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope A" Then
                    'CANVAS PIXEL GRID CODE
                    If TL45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TL45.Image
                        canvaseditor.Width = TL45.Width * 8 + 1
                        canvaseditor.Height = TL45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TL45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope B" Then
                    'CANVAS PIXEL GRID CODE
                    If TR45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TR45.Image
                        canvaseditor.Width = TR45.Width * 8 + 1
                        canvaseditor.Height = TR45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TR45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope C" Then
                    'CANVAS PIXEL GRID CODE
                    If BL45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BL45.Image
                        canvaseditor.Width = BL45.Width * 8 + 1
                        canvaseditor.Height = BL45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BL45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope D" Then
                    'CANVAS PIXEL GRID CODE
                    If BR45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BR45.Image
                        canvaseditor.Width = BR45.Width * 8 + 1
                        canvaseditor.Height = BR45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BR45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope A" Then
                    'CANVAS PIXEL GRID CODE
                    If TL26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TL26.Image
                        canvaseditor.Width = TL26.Width * 8 + 1
                        canvaseditor.Height = TL26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TL26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope B" Then
                    'CANVAS PIXEL GRID CODE
                    If TR26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TR26.Image
                        canvaseditor.Width = TR26.Width * 8 + 1
                        canvaseditor.Height = TR26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TR26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope C" Then
                    'CANVAS PIXEL GRID CODE
                    If BL26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BL26.Image
                        canvaseditor.Width = BL26.Width * 8 + 1
                        canvaseditor.Height = BL26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BL26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope D" Then
                    'CANVAS PIXEL GRID CODE
                    If BR26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BR26.Image
                        canvaseditor.Width = BR26.Width * 8 + 1
                        canvaseditor.Height = BR26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BR26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Inner 1" Then
                    'CANVAS PIXEL GRID CODE
                    If Inner26A.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = Inner26A.Image
                        canvaseditor.Width = Inner26A.Width * 8 + 1
                        canvaseditor.Height = Inner26A.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(Inner26A.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Inner 2" Then
                    'CANVAS PIXEL GRID CODE
                    If Inner26B.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = Inner26B.Image
                        canvaseditor.Width = Inner26B.Width * 8 + 1
                        canvaseditor.Height = Inner26B.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(Inner26B.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Slope A" Then
                    'CANVAS PIXEL GRID CODE
                    If TL14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TL14.Image
                        canvaseditor.Width = TL14.Width * 8 + 1
                        canvaseditor.Height = TL14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TL14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Slope B" Then
                    'CANVAS PIXEL GRID CODE
                    If TR14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TR14.Image
                        canvaseditor.Width = TR14.Width * 8 + 1
                        canvaseditor.Height = TR14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TR14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Inner 1" Then
                    'CANVAS PIXEL GRID CODE
                    If TLINNER14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TLINNER14.Image
                        canvaseditor.Width = TLINNER14.Width * 8 + 1
                        canvaseditor.Height = TLINNER14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TLINNER14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Inner 2" Then
                    'CANVAS PIXEL GRID CODE
                    If TRINNER14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TRINNER14.Image
                        canvaseditor.Width = TRINNER14.Width * 8 + 1
                        canvaseditor.Height = TRINNER14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TRINNER14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

            End If
        End If


        'If down = True Then
        'canvaseditor.CreateGraphics.FillRectangle(Brush, e.X, e.Y, 8, 8)
        'End If

        ' LocalMousePosition = canvaseditor.PointToClient(Cursor.Position)
        'Dim X As Integer
        'Dim Y As Integer

        'If LocalMousePosition.X > 0 And LocalMousePosition.X < 9 Then
        'X = 1
        ' ElseIf LocalMousePosition.X > 8 And LocalMousePosition.X < 17 Then
        'X = 2
        'ElseIf LocalMousePosition.X > 16 And LocalMousePosition.X < 25 Then
        'X = 3
        'End If

        'Label6.Text = (X & ", " & Y)
    End Sub


    Private Sub PaintBrush(ByVal X As Integer, ByVal Y As Integer)
        Try
            Using g As Graphics = Graphics.FromImage(bmpNew)
                g.FillRectangle(New SolidBrush(COLOR1), New Rectangle(X, Y, PENCIL, PENCIL))
            End Using
            canvaseditor.Refresh()
            BMP = bmpNew
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_MouseDown(ByVal ByValsender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown
        If e.Button = MouseButtons.Left Then




        End If
    End Sub

    Private Sub canvaseditor_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvaseditor.MouseDown

        Dim x As Integer
        Dim y As Integer
        x = (e.X \ 8) + 1
        y = (e.Y \ 8) + 1


        Label6.Text = "X:" & x & ", Y:" & y

        If e.Button = MouseButtons.Left Then
            If ComboBox2.SelectedIndex = 0 Then
            Else


                If RadioButton3.Checked = True Then
                    Dim Image1 As New Bitmap(canvaseditor.Image, canvaseditor.Width, canvaseditor.Height)

                    Using g As Graphics = Graphics.FromImage(Image1)
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                        g.DrawImage(BackgroundImage1, 0, 0, Image1.Width, Image1.Height)
                    End Using

                    COLOR1 = Image1.GetPixel(e.X, e.Y)
                    ColorDialog2.Color = COLOR1
                    PictureBox3.BackColor = COLOR1

                ElseIf RadioButton1.Checked = True Then
                    Draw = True
                    PaintBrush(e.X, e.Y)

                    Dim from_bm As New Bitmap(BMP)

                    ' Make the destination Bitmap.
                    Dim wid As Integer = BMP.Width \ 8
                    Dim hgt As Integer = BMP.Height \ 8
                    Dim to_bm As New Bitmap(wid, hgt)

                    ' Copy the image.
                    Dim gr As Graphics = Graphics.FromImage(to_bm)
                    gr.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                    gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    gr.DrawImage(from_bm, 0, 0, wid, hgt)




                    If ComboBox2.SelectedItem = "Top-Left" Then
                        topleft.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top" Then
                        top.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top-Right" Then
                        topright.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Left" Then
                        left.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Center" Then
                        center.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Right" Then
                        right.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Left" Then
                        bottomleft.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom" Then
                        bottom.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Right" Then
                        bottomright.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top-Left Inner" Then
                        inner1.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top-Right Inner" Then
                        inner2.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Left Inner" Then
                        inner3.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Right Inner" Then
                        inner4.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope A" Then
                        TL45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope B" Then
                        TR45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope C" Then
                        BL45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope D" Then
                        BR45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope A" Then
                        TL26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope B" Then
                        TR26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope C" Then
                        BL26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope D" Then
                        BR26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Inner 1" Then
                        Inner26A.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Inner 2" Then
                        Inner26B.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Slope A" Then
                        TL14.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Slope B" Then
                        TR14.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Inner 1" Then
                        TLINNER14.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Inner 2" Then
                        TRINNER14.Image = to_bm
                    End If

                ElseIf RadioButton2.Checked = True Then
                    SafeFloodFill(bmpNew, e.X, e.Y, COLOR1)

                   

                    BMP = bmpNew

                    Dim from_bm As New Bitmap(BMP)

                    ' Make the destination Bitmap.
                    Dim wid As Integer = BMP.Width \ 8
                    Dim hgt As Integer = BMP.Height \ 8
                    Dim to_bm As New Bitmap(wid, hgt)

                    ' Copy the image.
                    Dim gr As Graphics = Graphics.FromImage(to_bm)
                    gr.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                    gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    gr.DrawImage(from_bm, 0, 0, wid, hgt)




                    If ComboBox2.SelectedItem = "Top-Left" Then
                        topleft.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top" Then
                        top.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top-Right" Then
                        topright.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Left" Then
                        left.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Center" Then
                        center.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Right" Then
                        right.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Left" Then
                        bottomleft.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom" Then
                        bottom.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Right" Then
                        bottomright.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top-Left Inner" Then
                        inner1.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Top-Right Inner" Then
                        inner2.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Left Inner" Then
                        inner3.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "Bottom-Right Inner" Then
                        inner4.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope A" Then
                        TL45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope B" Then
                        TR45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope C" Then
                        BL45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "45 Slope D" Then
                        BR45.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope A" Then
                        TL26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope B" Then
                        TR26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope C" Then
                        BL26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Slope D" Then
                        BR26.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Inner 1" Then
                        Inner26A.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "26 Inner 2" Then
                        Inner26B.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Slope A" Then
                        TL14.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Slope B" Then
                        TR14.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Inner 1" Then
                        TLINNER14.Image = to_bm
                    ElseIf ComboBox2.SelectedItem = "14 Inner 2" Then
                        TRINNER14.Image = to_bm
                    End If

                End If

                If ComboBox2.SelectedItem = "Top-Left" Then
                    'CANVAS PIXEL GRID CODE
                    If topleft.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = topleft.Image
                        canvaseditor.Width = topleft.Width * 8 + 1
                        canvaseditor.Height = topleft.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(topleft.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top" Then
                    'CANVAS PIXEL GRID CODE
                    If top.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = top.Image
                        canvaseditor.Width = top.Width * 8 + 1
                        canvaseditor.Height = top.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(top.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top-Right" Then
                    'CANVAS PIXEL GRID CODE
                    If topright.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = topright.Image
                        canvaseditor.Width = topright.Width * 8 + 1
                        canvaseditor.Height = topright.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(topright.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Left" Then
                    'CANVAS PIXEL GRID CODE
                    If left.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = left.Image
                        canvaseditor.Width = left.Width * 8 + 1
                        canvaseditor.Height = left.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(left.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Center" Then
                    'CANVAS PIXEL GRID CODE
                    If center.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = center.Image
                        canvaseditor.Width = center.Width * 8 + 1
                        canvaseditor.Height = center.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(center.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Right" Then
                    'CANVAS PIXEL GRID CODE
                    If right.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = right.Image
                        canvaseditor.Width = right.Width * 8 + 1
                        canvaseditor.Height = right.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(right.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Left" Then
                    'CANVAS PIXEL GRID CODE
                    If bottomleft.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = bottomleft.Image
                        canvaseditor.Width = bottomleft.Width * 8 + 1
                        canvaseditor.Height = bottomleft.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(bottomleft.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom" Then
                    'CANVAS PIXEL GRID CODE
                    If bottom.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = bottom.Image
                        canvaseditor.Width = bottom.Width * 8 + 1
                        canvaseditor.Height = bottom.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(bottom.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Right" Then
                    'CANVAS PIXEL GRID CODE
                    If bottomright.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = bottomright.Image
                        canvaseditor.Width = bottomright.Width * 8 + 1
                        canvaseditor.Height = bottomright.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(bottomright.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top-Left Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner1.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner1.Image
                        canvaseditor.Width = inner1.Width * 8 + 1
                        canvaseditor.Height = inner1.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner1.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Top-Right Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner2.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner2.Image
                        canvaseditor.Width = inner2.Width * 8 + 1
                        canvaseditor.Height = inner2.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner2.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Left Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner3.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner3.Image
                        canvaseditor.Width = inner3.Width * 8 + 1
                        canvaseditor.Height = inner3.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner3.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "Bottom-Right Inner" Then
                    'CANVAS PIXEL GRID CODE
                    If inner4.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = inner4.Image
                        canvaseditor.Width = inner4.Width * 8 + 1
                        canvaseditor.Height = inner4.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(inner4.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope A" Then
                    'CANVAS PIXEL GRID CODE
                    If TL45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TL45.Image
                        canvaseditor.Width = TL45.Width * 8 + 1
                        canvaseditor.Height = TL45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TL45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope B" Then
                    'CANVAS PIXEL GRID CODE
                    If TR45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TR45.Image
                        canvaseditor.Width = TR45.Width * 8 + 1
                        canvaseditor.Height = TR45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TR45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope C" Then
                    'CANVAS PIXEL GRID CODE
                    If BL45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BL45.Image
                        canvaseditor.Width = BL45.Width * 8 + 1
                        canvaseditor.Height = BL45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BL45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "45 Slope D" Then
                    'CANVAS PIXEL GRID CODE
                    If BR45.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BR45.Image
                        canvaseditor.Width = BR45.Width * 8 + 1
                        canvaseditor.Height = BR45.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BR45.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope A" Then
                    'CANVAS PIXEL GRID CODE
                    If TL26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TL26.Image
                        canvaseditor.Width = TL26.Width * 8 + 1
                        canvaseditor.Height = TL26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TL26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope B" Then
                    'CANVAS PIXEL GRID CODE
                    If TR26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TR26.Image
                        canvaseditor.Width = TR26.Width * 8 + 1
                        canvaseditor.Height = TR26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TR26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope C" Then
                    'CANVAS PIXEL GRID CODE
                    If BL26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BL26.Image
                        canvaseditor.Width = BL26.Width * 8 + 1
                        canvaseditor.Height = BL26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BL26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Slope D" Then
                    'CANVAS PIXEL GRID CODE
                    If BR26.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = BR26.Image
                        canvaseditor.Width = BR26.Width * 8 + 1
                        canvaseditor.Height = BR26.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(BR26.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Inner 1" Then
                    'CANVAS PIXEL GRID CODE
                    If Inner26A.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = Inner26A.Image
                        canvaseditor.Width = Inner26A.Width * 8 + 1
                        canvaseditor.Height = Inner26A.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(Inner26A.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "26 Inner 2" Then
                    'CANVAS PIXEL GRID CODE
                    If Inner26B.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = Inner26B.Image
                        canvaseditor.Width = Inner26B.Width * 8 + 1
                        canvaseditor.Height = Inner26B.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(Inner26B.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Slope A" Then
                    'CANVAS PIXEL GRID CODE
                    If TL14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TL14.Image
                        canvaseditor.Width = TL14.Width * 8 + 1
                        canvaseditor.Height = TL14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TL14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Slope B" Then
                    'CANVAS PIXEL GRID CODE
                    If TR14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TR14.Image
                        canvaseditor.Width = TR14.Width * 8 + 1
                        canvaseditor.Height = TR14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TR14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Inner 1" Then
                    'CANVAS PIXEL GRID CODE
                    If TLINNER14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TLINNER14.Image
                        canvaseditor.Width = TLINNER14.Width * 8 + 1
                        canvaseditor.Height = TLINNER14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TLINNER14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If

                If ComboBox2.SelectedItem = "14 Inner 2" Then
                    'CANVAS PIXEL GRID CODE
                    If TRINNER14.Image Is Nothing Then
                    Else
                        canvaseditor.Image = Nothing
                        canvaseditor.Image = TRINNER14.Image
                        canvaseditor.Width = TRINNER14.Width * 8 + 1
                        canvaseditor.Height = TRINNER14.Height * 8 + 1
                        'load and draw the image(s) once
                        BackgroundImage1 = New Bitmap(TRINNER14.Image)
                        bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                        Using g As Graphics = Graphics.FromImage(bmpNew)
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                            g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                        End Using
                        canvaseditor.Focus()
                        GroupBox13.Focus()
                    End If
                End If


            End If

        End If

    End Sub



    Private Sub canvaseditor_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvaseditor.MouseUp
        Draw = False
        If RadioButton3.Checked = True Then
            RadioButton1.Checked = True
            Dim cur As Icon
            cur = My.Resources._1994
            Cursor = New Cursor(cur.Handle)
            defaultcurse = New Cursor(cur.Handle)
        End If

        If RadioButton2.Checked = True Then

            canvaseditor.Image = Nothing
            canvaseditor.Image = BMP
            canvaseditor.Refresh()
        End If
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        ColorDialog2.ShowDialog()
        COLOR1 = ColorDialog2.Color
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
    End Sub

    Private Sub canvaseditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles canvaseditor.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        My.Settings.CURRENTSELECTION = ComboBox2.SelectedItem

        If ComboBox2.SelectedIndex = 0 Then
            canvaseditor.Image = Nothing
        End If

        If ComboBox2.SelectedItem = "Top-Left" Then
            'CANVAS PIXEL GRID CODE
            If topleft.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = topleft.Image
                canvaseditor.Width = topleft.Width * 8 + 1
                canvaseditor.Height = topleft.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(topleft.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top" Then
            'CANVAS PIXEL GRID CODE
            If top.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = top.Image
                canvaseditor.Width = top.Width * 8 + 1
                canvaseditor.Height = top.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(top.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Right" Then
            'CANVAS PIXEL GRID CODE
            If topright.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = topright.Image
                canvaseditor.Width = topright.Width * 8 + 1
                canvaseditor.Height = topright.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(topright.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Left" Then
            'CANVAS PIXEL GRID CODE
            If left.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = left.Image
                canvaseditor.Width = left.Width * 8 + 1
                canvaseditor.Height = left.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(left.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Center" Then
            'CANVAS PIXEL GRID CODE
            If center.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = center.Image
                canvaseditor.Width = center.Width * 8 + 1
                canvaseditor.Height = center.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(center.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Right" Then
            'CANVAS PIXEL GRID CODE
            If right.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = right.Image
                canvaseditor.Width = right.Width * 8 + 1
                canvaseditor.Height = right.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(right.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Left" Then
            'CANVAS PIXEL GRID CODE
            If bottomleft.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottomleft.Image
                canvaseditor.Width = bottomleft.Width * 8 + 1
                canvaseditor.Height = bottomleft.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottomleft.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom" Then
            'CANVAS PIXEL GRID CODE
            If bottom.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottom.Image
                canvaseditor.Width = bottom.Width * 8 + 1
                canvaseditor.Height = bottom.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottom.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Right" Then
            'CANVAS PIXEL GRID CODE
            If bottomright.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottomright.Image
                canvaseditor.Width = bottomright.Width * 8 + 1
                canvaseditor.Height = bottomright.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottomright.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Left Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner1.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner1.Image
                canvaseditor.Width = inner1.Width * 8 + 1
                canvaseditor.Height = inner1.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner1.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Right Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner2.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner2.Image
                canvaseditor.Width = inner2.Width * 8 + 1
                canvaseditor.Height = inner2.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner2.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Left Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner3.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner3.Image
                canvaseditor.Width = inner3.Width * 8 + 1
                canvaseditor.Height = inner3.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner3.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Right Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner4.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner4.Image
                canvaseditor.Width = inner4.Width * 8 + 1
                canvaseditor.Height = inner4.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner4.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL45.Image
                canvaseditor.Width = TL45.Width * 8 + 1
                canvaseditor.Height = TL45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR45.Image
                canvaseditor.Width = TR45.Width * 8 + 1
                canvaseditor.Height = TR45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope C" Then
            'CANVAS PIXEL GRID CODE
            If BL45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BL45.Image
                canvaseditor.Width = BL45.Width * 8 + 1
                canvaseditor.Height = BL45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BL45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope D" Then
            'CANVAS PIXEL GRID CODE
            If BR45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BR45.Image
                canvaseditor.Width = BR45.Width * 8 + 1
                canvaseditor.Height = BR45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BR45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL26.Image
                canvaseditor.Width = TL26.Width * 8 + 1
                canvaseditor.Height = TL26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR26.Image
                canvaseditor.Width = TR26.Width * 8 + 1
                canvaseditor.Height = TR26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope C" Then
            'CANVAS PIXEL GRID CODE
            If BL26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BL26.Image
                canvaseditor.Width = BL26.Width * 8 + 1
                canvaseditor.Height = BL26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BL26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope D" Then
            'CANVAS PIXEL GRID CODE
            If BR26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BR26.Image
                canvaseditor.Width = BR26.Width * 8 + 1
                canvaseditor.Height = BR26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BR26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Inner 1" Then
            'CANVAS PIXEL GRID CODE
            If Inner26A.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = Inner26A.Image
                canvaseditor.Width = Inner26A.Width * 8 + 1
                canvaseditor.Height = Inner26A.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(Inner26A.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Inner 2" Then
            'CANVAS PIXEL GRID CODE
            If Inner26B.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = Inner26B.Image
                canvaseditor.Width = Inner26B.Width * 8 + 1
                canvaseditor.Height = Inner26B.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(Inner26B.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL14.Image
                canvaseditor.Width = TL14.Width * 8 + 1
                canvaseditor.Height = TL14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR14.Image
                canvaseditor.Width = TR14.Width * 8 + 1
                canvaseditor.Height = TR14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Inner 1" Then
            'CANVAS PIXEL GRID CODE
            If TLINNER14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TLINNER14.Image
                canvaseditor.Width = TLINNER14.Width * 8 + 1
                canvaseditor.Height = TLINNER14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TLINNER14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Inner 2" Then
            'CANVAS PIXEL GRID CODE
            If TRINNER14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TRINNER14.Image
                canvaseditor.Width = TRINNER14.Width * 8 + 1
                canvaseditor.Height = TRINNER14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TRINNER14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'FIRST METHOD

        'Dim BMP3 As New Bitmap(topleft.Image.Width, topleft.Image.Height, Drawing.Imaging.PixelFormat.Format48bppRgb)
        'Dim GRPH As Graphics = Graphics.FromImage(BMP3)
        'Dim strFile As String = "asshole.gif"

        ' Set the rendering of the graphics object to high quality.
        'GRPH.InterpolationMode = Drawing.Drawing2D.InterpolationMode.Default
        'GRPH.SmoothingMode = Drawing.Drawing2D.SmoothingMode.Default

        ' Draw the image to the graphics object. Because the graphics object has a reference to
        ' the bitmap object, then image will also be drawn on the bitmap.
        'GRPH.DrawImage(topleft.Image, New Rectangle(0, 0, topleft.Image.Width, topleft.Image.Height), New Rectangle(0, 0, topleft.Image.Size.Width, topleft.Image.Size.Height), GraphicsUnit.Pixel)

        'BMP3.Save(strFile, Drawing.Imaging.ImageFormat.Gif)

        'SECOND METHOD

        ' BMP2 = topleft.Image
        ' BMP2.Save("fuckoff.png", Imaging.ImageFormat.Png)
        ' BMP2 = New Bitmap(topleft.Width, topleft.Height)

        'THIRD METHOD WORKS!!!!

        FolderBrowserDialog2.ShowDialog()
        Dim MYPATH As String = FolderBrowserDialog2.SelectedPath

       
        If ComboBox1.SelectedItem = "SMB3-Wood" Then
            RENDER_SMB3_WOOD(MYPATH)
        End If

        If ComboBox1.SelectedItem = "SMB3-Grass" Then
            RENDER_SMB3_GRASS(MYPATH)
        End If

        If ComboBox1.SelectedItem = "SMB3-Cave" Then
            RENDER_SMB3_CAVE(MYPATH)
        End If

        If ComboBox1.SelectedItem = "SMB3-Desert" Then
            RENDER_SMB3_DESERT(MYPATH)
        End If

        If ComboBox1.SelectedItem = "SMW-Grass" Then
            RENDER_SMW_GRASS(MYPATH)
        End If

        If ComboBox1.SelectedItem = "SMW-Cave" Then
            RENDER_SMW_CAVE(MYPATH)
        End If

        If ComboBox1.SelectedItem = "SMW-Castle" Then
            RENDER_SMW_CASTLE(MYPATH)
        End If
    End Sub



    Private Sub RENDER_SMB3_WOOD(ByVal MYPATH As String)

        Dim newBitmap As Bitmap = New Bitmap(topleft.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap.Save(MYPATH & "\" & "block-7.gif", imgCodecs(0), imgParams)


        Dim newBitmap2 As Bitmap = New Bitmap(top.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs2() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams2 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality2 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap2.Save(MYPATH & "\" & "block-3.gif", imgCodecs(0), imgParams)

        Dim newBitmap3 As Bitmap = New Bitmap(topright.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs3() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams3 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality3 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap3.Save(MYPATH & "\" & "block-6.gif", imgCodecs(0), imgParams)

        Dim newBitmap4 As Bitmap = New Bitmap(left.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs4() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams4 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality4 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap4.Save(MYPATH & "\" & "block-15.gif", imgCodecs(0), imgParams)

        Dim newBitmap5 As Bitmap = New Bitmap(center.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs5() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams5 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality5 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap5.Save(MYPATH & "\" & "block-16.gif", imgCodecs(0), imgParams)

        Dim newBitmap6 As Bitmap = New Bitmap(right.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs6() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams6 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality6 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap6.Save(MYPATH & "\" & "block-17.gif", imgCodecs(0), imgParams)

        Dim newBitmap7 As Bitmap = New Bitmap(bottomleft.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs7() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams7 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality7 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap7.Save(MYPATH & "\" & "block-274.gif", imgCodecs(0), imgParams)

        Dim newBitmap8 As Bitmap = New Bitmap(bottom.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs8() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams8 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality8 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap8.Save(MYPATH & "\" & "block-276.gif", imgCodecs(0), imgParams)

        Dim newBitmap9 As Bitmap = New Bitmap(bottomright.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs9() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams9 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality9 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap9.Save(MYPATH & "\" & "block-275.gif", imgCodecs(0), imgParams)

        Dim newBitmap10 As Bitmap = New Bitmap(inner3.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs10() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams10 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality10 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap10.Save(MYPATH & "\" & "block-603.gif", imgCodecs(0), imgParams)

        Dim newBitmap11 As Bitmap = New Bitmap(inner4.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs11() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams11 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality11 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap11.Save(MYPATH & "\" & "block-602.gif", imgCodecs(0), imgParams)

        Dim newBitmap12 As Bitmap = New Bitmap(TL45.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs12() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams12 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality12 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap12.Save(MYPATH & "\" & "block-600.gif", imgCodecs(0), imgParams)

        Dim newBitmap13 As Bitmap = New Bitmap(TR45.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs13() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams13 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality13 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap13.Save(MYPATH & "\" & "block-601.gif", imgCodecs(0), imgParams)

        Dim newBitmap14 As Bitmap = New Bitmap(TL26.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs14() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams14 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality14 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap14.Save(MYPATH & "\" & "block-604.gif", imgCodecs(0), imgParams)

        Dim newBitmap15 As Bitmap = New Bitmap(TR26.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs15() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams15 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality15 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap15.Save(MYPATH & "\" & "block-605.gif", imgCodecs(0), imgParams)

        Dim newBitmap16 As Bitmap = New Bitmap(Inner26A.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs16() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams16 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality16 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap16.Save(MYPATH & "\" & "block-606.gif", imgCodecs(0), imgParams)

        Dim newBitmap17 As Bitmap = New Bitmap(Inner26B.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs17() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams17 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality17 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap17.Save(MYPATH & "\" & "block-607.gif", imgCodecs(0), imgParams)


    End Sub


    Private Sub RENDER_SMB3_GRASS(ByVal MYPATH As String)
        'Dim smb3grass() As String = {"block-9.gif", "block-10.gif"}
        'Dim position() As Bitmap = {topleft.Image, top.Image}

        'For Each item In smb3grass

        Dim newBitmap As Bitmap = New Bitmap(topleft.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap.Save(MYPATH & "\" & "block-9.gif", imgCodecs(0), imgParams)

        Dim newBitmap2 As Bitmap = New Bitmap(top.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs2() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams2 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality2 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap2.Save(MYPATH & "\" & "block-10.gif", imgCodecs(0), imgParams)

        Dim newBitmap3 As Bitmap = New Bitmap(topright.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs3() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams3 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality3 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap3.Save(MYPATH & "\" & "block-11.gif", imgCodecs(0), imgParams)

        Dim newBitmap4 As Bitmap = New Bitmap(left.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs4() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams4 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality4 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap4.Save(MYPATH & "\" & "block-18.gif", imgCodecs(0), imgParams)

        Dim newBitmap5 As Bitmap = New Bitmap(center.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs5() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams5 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality5 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap5.Save(MYPATH & "\" & "block-19.gif", imgCodecs(0), imgParams)

        Dim newBitmap6 As Bitmap = New Bitmap(right.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs6() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams6 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality6 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap6.Save(MYPATH & "\" & "block-20.gif", imgCodecs(0), imgParams)

        Dim newBitmap7 As Bitmap = New Bitmap(bottomleft.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs7() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams7 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality7 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap7.Save(MYPATH & "\" & "block-279.gif", imgCodecs(0), imgParams)

        Dim newBitmap8 As Bitmap = New Bitmap(bottom.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs8() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams8 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality8 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap8.Save(MYPATH & "\" & "block-278.gif", imgCodecs(0), imgParams)

        Dim newBitmap9 As Bitmap = New Bitmap(bottomright.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs9() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams9 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality9 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap9.Save(MYPATH & "\" & "block-277.gif", imgCodecs(0), imgParams)

        Dim newBitmap10 As Bitmap = New Bitmap(TL45.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs10() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams10 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality10 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap10.Save(MYPATH & "\" & "block-305.gif", imgCodecs(0), imgParams)

        Dim newBitmap11 As Bitmap = New Bitmap(TR45.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs11() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams11 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality11 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 110)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap11.Save(MYPATH & "\" & "block-307.gif", imgCodecs(0), imgParams)

        Dim newBitmap12 As Bitmap = New Bitmap(BL45.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs12() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams12 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality12 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 120)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap12.Save(MYPATH & "\" & "block-311.gif", imgCodecs(0), imgParams)

        Dim newBitmap13 As Bitmap = New Bitmap(BR45.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs13() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams13 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality13 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 130)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap13.Save(MYPATH & "\" & "block-313.gif", imgCodecs(0), imgParams)

        Dim newBitmap14 As Bitmap = New Bitmap(TL26.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs14() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams14 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality14 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 140)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap14.Save(MYPATH & "\" & "block-306.gif", imgCodecs(0), imgParams)

        Dim newBitmap15 As Bitmap = New Bitmap(TR26.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs15() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams15 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality15 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 150)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap15.Save(MYPATH & "\" & "block-308.gif", imgCodecs(0), imgParams)

        Dim newBitmap16 As Bitmap = New Bitmap(BL26.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs16() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams16 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality16 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 160)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap16.Save(MYPATH & "\" & "block-312.gif", imgCodecs(0), imgParams)

        Dim newBitmap17 As Bitmap = New Bitmap(BR26.Image)
        ' Get an array of ImageEncoders..array item 1 is Jpeg.
        Dim imgCodecs17() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()
        ' Set quality Parameter for the Jpeg codec
        Dim imgParams17 As EncoderParameters = New EncoderParameters(1)
        Dim imgQuality17 As EncoderParameter = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 170)
        ' Set quality
        imgParams.Param(0) = imgQuality
        ' Render BitMap Stream Back To Client
        newBitmap17.Save(MYPATH & "\" & "block-314.gif", imgCodecs(0), imgParams)




    End Sub


    Private Sub RENDER_SMB3_CAVE(ByVal MYPATH As String)

    End Sub


    Private Sub RENDER_SMB3_DESERT(ByVal MYPATH As String)

    End Sub



    Private Sub RENDER_SMW_GRASS(ByVal MYPATH As String)

    End Sub



    Private Sub RENDER_SMW_CAVE(ByVal MYPATH As String)

    End Sub


    Private Sub RENDER_SMW_CASTLE(ByVal MYPATH As String)

    End Sub










    Private Sub Inner26A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Inner26A.Click
        ComboBox2.SelectedItem = "26 Inner 1"
    End Sub

    Private Sub Inner26B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Inner26B.Click
        ComboBox2.SelectedItem = "26 Inner 2"
    End Sub

    Private Sub TLINNER14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TLINNER14.Click
        'CANVAS PIXEL GRID CODE
        If TLINNER14.Image Is Nothing Then
        Else
            canvaseditor.Image = Nothing
            canvaseditor.Image = TLINNER14.Image
            canvaseditor.Width = TLINNER14.Width * 8 + 1
            canvaseditor.Height = TLINNER14.Height * 8 + 1
            'load and draw the image(s) once
            BackgroundImage1 = New Bitmap(TLINNER14.Image)
            bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
            Using g As Graphics = Graphics.FromImage(bmpNew)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
            End Using
            canvaseditor.Focus()
            GroupBox13.Focus()
            ComboBox2.SelectedItem = "14 Inner 1"
            Me.CenterToScreen()
        End If

        
    End Sub

    Private Sub TRINNER14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TRINNER14.Click
        'CANVAS PIXEL GRID CODE
        If TRINNER14.Image Is Nothing Then
        Else
            canvaseditor.Image = Nothing
            canvaseditor.Image = TRINNER14.Image
            canvaseditor.Width = TRINNER14.Width * 8 + 1
            canvaseditor.Height = TRINNER14.Height * 8 + 1
            'load and draw the image(s) once
            BackgroundImage1 = New Bitmap(TRINNER14.Image)
            bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
            Using g As Graphics = Graphics.FromImage(bmpNew)
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
            End Using
            canvaseditor.Focus()
            GroupBox13.Focus()
            ComboBox2.SelectedItem = "14 Inner 2"
            Me.CenterToScreen()
        End If

        
    End Sub

    

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        C1.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        C2.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        C3.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        C4.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        C5.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        C6.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        C7.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        C8.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        C9.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        C10.BackColor = COLOR1
        GroupBox17.Focus()
    End Sub

    Private Sub C1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1.Click
        COLOR1 = C1.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C2.Click
        COLOR1 = C2.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C3.Click
        COLOR1 = C3.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C4.Click
        COLOR1 = C4.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C5.Click
        COLOR1 = C5.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C6.Click
        COLOR1 = C6.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C7.Click
        COLOR1 = C7.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C8.Click
        COLOR1 = C8.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C9.Click
        COLOR1 = C9.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub C10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C10.Click
        COLOR1 = C10.BackColor
        PictureBox3.BackColor = COLOR1
        Brush = New SolidBrush(COLOR1)
        ColorDialog2.Color = COLOR1
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        My.Computer.Audio.Play(My.Resources.smw_magikoopa_beam, AudioPlayMode.Background)

        ListBox1.Items.Clear()
        My.Settings.INPATH = Environment.CurrentDirectory & "\block"
        My.Settings.Save()

        Try
            Me.Text = "Kuribo Red"
            TextBox1.Text = My.Settings.INPATH
            Dim di As New DirectoryInfo(My.Settings.INPATH)
            Dim folder As FileInfo() = di.GetFiles()
            For Each file In folder
                ListBox1.Items.Add(file)
            Next
        Catch ex As Exception

        End Try

        Try
            tiledcanvas.BackgroundImage = Nothing
            topleft.Image = Nothing
            top.Image = Nothing
            topright.Image = Nothing
            left.Image = Nothing
            center.Image = Nothing
            right.Image = Nothing
            bottomleft.Image = Nothing
            bottom.Image = Nothing
            bottomright.Image = Nothing
            inner1.Image = Nothing
            inner2.Image = Nothing
            inner3.Image = Nothing
            inner4.Image = Nothing
            innerA.Image = Nothing
            innerB.Image = Nothing
            innerC.Image = Nothing
            innerD.Image = Nothing
            TL45.Image = Nothing
            TR45.Image = Nothing
            BL45.Image = Nothing
            BR45.Image = Nothing
            SLOPE45A.Image = Nothing
            SLOPE45B.Image = Nothing
            SLOPE45C.Image = Nothing
            SLOPE45D.Image = Nothing
            TL26.Image = Nothing
            TR26.Image = Nothing
            BL26.Image = Nothing
            BR26.Image = Nothing
            SLOPE26A.Image = Nothing
            SLOPE26B.Image = Nothing
            SLOPE26C.Image = Nothing
            SLOPE26D.Image = Nothing
            Inner26A.Image = Nothing
            Inner26B.Image = Nothing
            TL14.Image = Nothing
            TR14.Image = Nothing
            TLINNER14.Image = Nothing
            TRINNER14.Image = Nothing
            SLOPE14A.Image = Nothing
            SLOPE14B.Image = Nothing
            SLOPE14C.Image = Nothing
            SLOPE14D.Image = Nothing

            If ComboBox1.SelectedItem = "SMW-Grass" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-80.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-81.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-82.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-83.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-87.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-84.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-265.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-264.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-266.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-273.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-263.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-85.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-86.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-299.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-300.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-309.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-310.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-616.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-617.gif")
                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-619.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-618.gif")
            End If

            If ComboBox1.SelectedItem = "SMW-Cave" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-246.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-250.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-247.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-252.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-251.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-253.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-248.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-254.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-249.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-256.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-255.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-257.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-258.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-316.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-315.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-317.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-318.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-365.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-366.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-368.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-367.gif")

                TL14.Image = Image.FromFile(My.Settings.INPATH & "\block-321.gif")
                TR14.Image = Image.FromFile(My.Settings.INPATH & "\block-319.gif")
                TLINNER14.Image = Image.FromFile(My.Settings.INPATH & "\block-320.gif")
                TRINNER14.Image = Image.FromFile(My.Settings.INPATH & "\block-322.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Wood" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-7.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-3.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-6.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-15.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-16.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-17.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-274.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-276.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-275.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-600.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-601.gif")

                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-602.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-603.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-604.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-605.gif")
                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-607.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-606.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Grass" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-9.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-10.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-11.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-18.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-19.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-20.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-279.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-278.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-277.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-305.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-307.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-311.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-313.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-306.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-308.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-312.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-314.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Cave" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-344.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-345.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-346.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-347.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-348.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-349.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-350.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-351.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-352.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-353.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-354.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-355.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-356.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-358.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-359.gif")
                BL45.Image = Image.FromFile(My.Settings.INPATH & "\block-362.gif")
                BR45.Image = Image.FromFile(My.Settings.INPATH & "\block-363.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-357.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-360.gif")
                BL26.Image = Image.FromFile(My.Settings.INPATH & "\block-361.gif")
                BR26.Image = Image.FromFile(My.Settings.INPATH & "\block-364.gif")
            End If

            If ComboBox1.SelectedItem = "SMB3-Desert" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-162.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-163.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-164.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-165.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-166.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-167.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-286.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-285.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-284.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-635.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-637.gif")

                TL26.Image = Image.FromFile(My.Settings.INPATH & "\block-636.gif")
                TR26.Image = Image.FromFile(My.Settings.INPATH & "\block-638.gif")
            End If

            If ComboBox1.SelectedItem = "SMW-Castle" Then
                topleft.Image = Image.FromFile(My.Settings.INPATH & "\block-425.gif")
                top.Image = Image.FromFile(My.Settings.INPATH & "\block-424.gif")
                topright.Image = Image.FromFile(My.Settings.INPATH & "\block-426.gif")
                left.Image = Image.FromFile(My.Settings.INPATH & "\block-423.gif")
                center.Image = Image.FromFile(My.Settings.INPATH & "\block-422.gif")
                right.Image = Image.FromFile(My.Settings.INPATH & "\block-421.gif")
                bottomleft.Image = Image.FromFile(My.Settings.INPATH & "\block-427.gif")
                bottom.Image = Image.FromFile(My.Settings.INPATH & "\block-419.gif")
                bottomright.Image = Image.FromFile(My.Settings.INPATH & "\block-436.gif")

                inner1.Image = Image.FromFile(My.Settings.INPATH & "\block-418.gif")
                inner2.Image = Image.FromFile(My.Settings.INPATH & "\block-417.gif")
                inner3.Image = Image.FromFile(My.Settings.INPATH & "\block-416.gif")
                inner4.Image = Image.FromFile(My.Settings.INPATH & "\block-415.gif")

                TL45.Image = Image.FromFile(My.Settings.INPATH & "\block-452.gif")
                TR45.Image = Image.FromFile(My.Settings.INPATH & "\block-451.gif")

                Inner26A.Image = Image.FromFile(My.Settings.INPATH & "\block-449.gif")
                Inner26B.Image = Image.FromFile(My.Settings.INPATH & "\block-450.gif")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then


        Else

        End If
    End Sub

   
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If NumericUpDown1.Value = 1 Then
            PENCIL = 8
        ElseIf NumericUpDown1.Value = 2 Then
            PENCIL = 16
        ElseIf NumericUpDown1.Value = 3 Then
            PENCIL = 32
        End If
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        If ComboBox2.SelectedIndex = 0 Then
            Dim result As Integer = MessageBox.Show("There is no tile selected. You cannot clear an empty canvas.", "Operation: Clear Canvas - Kuribo Red", MessageBoxButtons.OK)

        Else
            My.Computer.Audio.Play(My.Resources.smw_message_block, AudioPlayMode.Background)
            Dim result As Integer = MessageBox.Show("Do you really wish to clear the canvas? You cannot undo this event.", "Clear Canvas - Kuribo Red", MessageBoxButtons.YesNo)

            If result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then
                My.Computer.Audio.Play(My.Resources.smw_reserve_item_store, AudioPlayMode.Background)
                PENCIL = 5000
                COLOR1 = Color.White
                PaintBrush(0, 0)
                COLOR1 = PictureBox3.BackColor
                PENCIL = 8
            End If
        End If

    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
       
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

        If My.Settings.CURRENTSELECTION = "Top-Left" Then
            form2.show()
        ElseIf My.Settings.CURRENTSELECTION = "Top-Right" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Left" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Right" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "Top-Left Inner" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "Top-Right Inner" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Left Inner" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Right Inner" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "45 Slope A" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "45 Slope B" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "45 Slope C" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "45 Slope D" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "26 Slope A" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "26 Slope B" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "26 Slope C" Then
            Form2.Show()
        ElseIf My.Settings.CURRENTSELECTION = "26 Slope D" Then
            Form2.Show()
        Else



            If ComboBox2.SelectedIndex = 0 Then
                Dim result As Integer = MessageBox.Show("You must first select a tile before you can Flip Copy it.", "Operation: Flip Copy - Kuribo Red", MessageBoxButtons.OK)
            Else

                Dim result As Integer = MessageBox.Show("This will make a flipped copy of this tile to it's opposite-sided tile. This saves time when editing but is irreversible. Do you wish to continue?", "Operation: Flip Copy - Kuribo Red", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                ElseIf result = DialogResult.Yes Then
                    If My.Settings.CURRENTSELECTION = "Top" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(top.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipY)
                        bottom.Image = BMPTRANSFORM
                    End If
                    If My.Settings.CURRENTSELECTION = "Bottom" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(bottom.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipY)
                        top.Image = BMPTRANSFORM
                    End If
                    If My.Settings.CURRENTSELECTION = "Left" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(left.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        right.Image = BMPTRANSFORM
                    End If
                    If My.Settings.CURRENTSELECTION = "Right" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(right.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        left.Image = BMPTRANSFORM
                    End If



                    If My.Settings.CURRENTSELECTION = "26 Inner 1" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(Inner26A.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        Inner26B.Image = BMPTRANSFORM
                    End If
                    If My.Settings.CURRENTSELECTION = "26 Inner 2" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(Inner26B.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        Inner26A.Image = BMPTRANSFORM
                    End If



                    If My.Settings.CURRENTSELECTION = "14 Slope A" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(TL14.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        TR14.Image = BMPTRANSFORM
                    End If
                    If My.Settings.CURRENTSELECTION = "14 Slope B" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(TR14.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        TL14.Image = BMPTRANSFORM
                    End If





                    If My.Settings.CURRENTSELECTION = "14 Inner 1" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(TLINNER14.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        TRINNER14.Image = BMPTRANSFORM
                    End If
                    If My.Settings.CURRENTSELECTION = "14 Inner 2" Then
                        Dim BMPTRANSFORM As Bitmap = New Bitmap(TRINNER14.Image)
                        BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        TLINNER14.Image = BMPTRANSFORM
                    End If

                End If
            End If
        End If





    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        If ComboBox2.SelectedIndex = 0 Then
            Dim result As Integer = MessageBox.Show("You must first select a tile before you can Flip it.", "Operation: Flip X - Kuribo Red", MessageBoxButtons.OK)
        Else
            If My.Settings.CURRENTSELECTION = "Top-Left" Then
                Dim BMPTRANSFORM As Bitmap = New Bitmap(topleft.Image)
                BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipX)
                topleft.Image = BMPTRANSFORM
            End If
            End If


        canvaseditor.Image = Nothing

        If ComboBox2.SelectedItem = "Top-Left" Then
            'CANVAS PIXEL GRID CODE
            If topleft.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = topleft.Image
                canvaseditor.Width = topleft.Width * 8 + 1
                canvaseditor.Height = topleft.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(topleft.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top" Then
            'CANVAS PIXEL GRID CODE
            If top.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = top.Image
                canvaseditor.Width = top.Width * 8 + 1
                canvaseditor.Height = top.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(top.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Right" Then
            'CANVAS PIXEL GRID CODE
            If topright.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = topright.Image
                canvaseditor.Width = topright.Width * 8 + 1
                canvaseditor.Height = topright.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(topright.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Left" Then
            'CANVAS PIXEL GRID CODE
            If left.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = left.Image
                canvaseditor.Width = left.Width * 8 + 1
                canvaseditor.Height = left.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(left.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Center" Then
            'CANVAS PIXEL GRID CODE
            If center.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = center.Image
                canvaseditor.Width = center.Width * 8 + 1
                canvaseditor.Height = center.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(center.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Right" Then
            'CANVAS PIXEL GRID CODE
            If right.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = right.Image
                canvaseditor.Width = right.Width * 8 + 1
                canvaseditor.Height = right.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(right.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Left" Then
            'CANVAS PIXEL GRID CODE
            If bottomleft.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottomleft.Image
                canvaseditor.Width = bottomleft.Width * 8 + 1
                canvaseditor.Height = bottomleft.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottomleft.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom" Then
            'CANVAS PIXEL GRID CODE
            If bottom.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottom.Image
                canvaseditor.Width = bottom.Width * 8 + 1
                canvaseditor.Height = bottom.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottom.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Right" Then
            'CANVAS PIXEL GRID CODE
            If bottomright.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottomright.Image
                canvaseditor.Width = bottomright.Width * 8 + 1
                canvaseditor.Height = bottomright.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottomright.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Left Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner1.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner1.Image
                canvaseditor.Width = inner1.Width * 8 + 1
                canvaseditor.Height = inner1.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner1.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Right Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner2.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner2.Image
                canvaseditor.Width = inner2.Width * 8 + 1
                canvaseditor.Height = inner2.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner2.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Left Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner3.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner3.Image
                canvaseditor.Width = inner3.Width * 8 + 1
                canvaseditor.Height = inner3.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner3.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Right Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner4.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner4.Image
                canvaseditor.Width = inner4.Width * 8 + 1
                canvaseditor.Height = inner4.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner4.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL45.Image
                canvaseditor.Width = TL45.Width * 8 + 1
                canvaseditor.Height = TL45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR45.Image
                canvaseditor.Width = TR45.Width * 8 + 1
                canvaseditor.Height = TR45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope C" Then
            'CANVAS PIXEL GRID CODE
            If BL45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BL45.Image
                canvaseditor.Width = BL45.Width * 8 + 1
                canvaseditor.Height = BL45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BL45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope D" Then
            'CANVAS PIXEL GRID CODE
            If BR45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BR45.Image
                canvaseditor.Width = BR45.Width * 8 + 1
                canvaseditor.Height = BR45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BR45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL26.Image
                canvaseditor.Width = TL26.Width * 8 + 1
                canvaseditor.Height = TL26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR26.Image
                canvaseditor.Width = TR26.Width * 8 + 1
                canvaseditor.Height = TR26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope C" Then
            'CANVAS PIXEL GRID CODE
            If BL26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BL26.Image
                canvaseditor.Width = BL26.Width * 8 + 1
                canvaseditor.Height = BL26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BL26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope D" Then
            'CANVAS PIXEL GRID CODE
            If BR26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BR26.Image
                canvaseditor.Width = BR26.Width * 8 + 1
                canvaseditor.Height = BR26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BR26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Inner 1" Then
            'CANVAS PIXEL GRID CODE
            If Inner26A.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = Inner26A.Image
                canvaseditor.Width = Inner26A.Width * 8 + 1
                canvaseditor.Height = Inner26A.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(Inner26A.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Inner 2" Then
            'CANVAS PIXEL GRID CODE
            If Inner26B.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = Inner26B.Image
                canvaseditor.Width = Inner26B.Width * 8 + 1
                canvaseditor.Height = Inner26B.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(Inner26B.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL14.Image
                canvaseditor.Width = TL14.Width * 8 + 1
                canvaseditor.Height = TL14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR14.Image
                canvaseditor.Width = TR14.Width * 8 + 1
                canvaseditor.Height = TR14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Inner 1" Then
            'CANVAS PIXEL GRID CODE
            If TLINNER14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TLINNER14.Image
                canvaseditor.Width = TLINNER14.Width * 8 + 1
                canvaseditor.Height = TLINNER14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TLINNER14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Inner 2" Then
            'CANVAS PIXEL GRID CODE
            If TRINNER14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TRINNER14.Image
                canvaseditor.Width = TRINNER14.Width * 8 + 1
                canvaseditor.Height = TRINNER14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TRINNER14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        If ComboBox2.SelectedIndex = 0 Then
            Dim result As Integer = MessageBox.Show("You must first select a tile before you can Flip it.", "Operation: Flip Y - Kuribo Red", MessageBoxButtons.OK)
        Else
            If My.Settings.CURRENTSELECTION = "Top-Left" Then
                Dim BMPTRANSFORM As Bitmap = New Bitmap(topleft.Image)
                BMPTRANSFORM.RotateFlip(RotateFlipType.RotateNoneFlipY)
                topleft.Image = BMPTRANSFORM
            End If
            End If


        canvaseditor.Image = Nothing

        If ComboBox2.SelectedItem = "Top-Left" Then
            'CANVAS PIXEL GRID CODE
            If topleft.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = topleft.Image
                canvaseditor.Width = topleft.Width * 8 + 1
                canvaseditor.Height = topleft.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(topleft.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top" Then
            'CANVAS PIXEL GRID CODE
            If top.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = top.Image
                canvaseditor.Width = top.Width * 8 + 1
                canvaseditor.Height = top.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(top.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Right" Then
            'CANVAS PIXEL GRID CODE
            If topright.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = topright.Image
                canvaseditor.Width = topright.Width * 8 + 1
                canvaseditor.Height = topright.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(topright.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Left" Then
            'CANVAS PIXEL GRID CODE
            If left.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = left.Image
                canvaseditor.Width = left.Width * 8 + 1
                canvaseditor.Height = left.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(left.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Center" Then
            'CANVAS PIXEL GRID CODE
            If center.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = center.Image
                canvaseditor.Width = center.Width * 8 + 1
                canvaseditor.Height = center.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(center.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Right" Then
            'CANVAS PIXEL GRID CODE
            If right.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = right.Image
                canvaseditor.Width = right.Width * 8 + 1
                canvaseditor.Height = right.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(right.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Left" Then
            'CANVAS PIXEL GRID CODE
            If bottomleft.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottomleft.Image
                canvaseditor.Width = bottomleft.Width * 8 + 1
                canvaseditor.Height = bottomleft.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottomleft.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom" Then
            'CANVAS PIXEL GRID CODE
            If bottom.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottom.Image
                canvaseditor.Width = bottom.Width * 8 + 1
                canvaseditor.Height = bottom.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottom.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Right" Then
            'CANVAS PIXEL GRID CODE
            If bottomright.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = bottomright.Image
                canvaseditor.Width = bottomright.Width * 8 + 1
                canvaseditor.Height = bottomright.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(bottomright.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Left Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner1.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner1.Image
                canvaseditor.Width = inner1.Width * 8 + 1
                canvaseditor.Height = inner1.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner1.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Top-Right Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner2.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner2.Image
                canvaseditor.Width = inner2.Width * 8 + 1
                canvaseditor.Height = inner2.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner2.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Left Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner3.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner3.Image
                canvaseditor.Width = inner3.Width * 8 + 1
                canvaseditor.Height = inner3.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner3.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "Bottom-Right Inner" Then
            'CANVAS PIXEL GRID CODE
            If inner4.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = inner4.Image
                canvaseditor.Width = inner4.Width * 8 + 1
                canvaseditor.Height = inner4.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(inner4.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL45.Image
                canvaseditor.Width = TL45.Width * 8 + 1
                canvaseditor.Height = TL45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR45.Image
                canvaseditor.Width = TR45.Width * 8 + 1
                canvaseditor.Height = TR45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope C" Then
            'CANVAS PIXEL GRID CODE
            If BL45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BL45.Image
                canvaseditor.Width = BL45.Width * 8 + 1
                canvaseditor.Height = BL45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BL45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "45 Slope D" Then
            'CANVAS PIXEL GRID CODE
            If BR45.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BR45.Image
                canvaseditor.Width = BR45.Width * 8 + 1
                canvaseditor.Height = BR45.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BR45.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL26.Image
                canvaseditor.Width = TL26.Width * 8 + 1
                canvaseditor.Height = TL26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR26.Image
                canvaseditor.Width = TR26.Width * 8 + 1
                canvaseditor.Height = TR26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope C" Then
            'CANVAS PIXEL GRID CODE
            If BL26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BL26.Image
                canvaseditor.Width = BL26.Width * 8 + 1
                canvaseditor.Height = BL26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BL26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Slope D" Then
            'CANVAS PIXEL GRID CODE
            If BR26.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = BR26.Image
                canvaseditor.Width = BR26.Width * 8 + 1
                canvaseditor.Height = BR26.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(BR26.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Inner 1" Then
            'CANVAS PIXEL GRID CODE
            If Inner26A.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = Inner26A.Image
                canvaseditor.Width = Inner26A.Width * 8 + 1
                canvaseditor.Height = Inner26A.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(Inner26A.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "26 Inner 2" Then
            'CANVAS PIXEL GRID CODE
            If Inner26B.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = Inner26B.Image
                canvaseditor.Width = Inner26B.Width * 8 + 1
                canvaseditor.Height = Inner26B.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(Inner26B.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Slope A" Then
            'CANVAS PIXEL GRID CODE
            If TL14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TL14.Image
                canvaseditor.Width = TL14.Width * 8 + 1
                canvaseditor.Height = TL14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TL14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Slope B" Then
            'CANVAS PIXEL GRID CODE
            If TR14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TR14.Image
                canvaseditor.Width = TR14.Width * 8 + 1
                canvaseditor.Height = TR14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TR14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Inner 1" Then
            'CANVAS PIXEL GRID CODE
            If TLINNER14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TLINNER14.Image
                canvaseditor.Width = TLINNER14.Width * 8 + 1
                canvaseditor.Height = TLINNER14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TLINNER14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If

        If ComboBox2.SelectedItem = "14 Inner 2" Then
            'CANVAS PIXEL GRID CODE
            If TRINNER14.Image Is Nothing Then
            Else
                canvaseditor.Image = Nothing
                canvaseditor.Image = TRINNER14.Image
                canvaseditor.Width = TRINNER14.Width * 8 + 1
                canvaseditor.Height = TRINNER14.Height * 8 + 1
                'load and draw the image(s) once
                BackgroundImage1 = New Bitmap(TRINNER14.Image)
                bmpNew = New Bitmap(canvaseditor.Width * scaleFactor, canvaseditor.Height * scaleFactor)
                Using g As Graphics = Graphics.FromImage(bmpNew)
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
                    g.DrawImage(BackgroundImage1, 0, 0, bmpNew.Width, bmpNew.Height)
                End Using
                canvaseditor.Focus()
                GroupBox13.Focus()
            End If
        End If
    End Sub

   

    Private Sub TextBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.MouseEnter
        ToolTip1.SetToolTip(TextBox1, TextBox1.Text)
    End Sub

 
    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        My.Computer.Audio.Play(My.Resources.nsmb_pipe, AudioPlayMode.Background)
        Process.Start("http://wiztechsolutions.webs.com/downloads.html")
    End Sub



    Private Sub tiledcanvas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tiledcanvas.Click

    End Sub

    Private Sub canvaseditor_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvaseditor.MouseWheel

    End Sub
End Class
