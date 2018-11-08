Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Form1.inner1.Image Is Nothing Then
            Form1.inner1.Image = My.Resources.transblank
        End If
        If Form1.inner2.Image Is Nothing Then
            Form1.inner2.Image = My.Resources.transblank
        End If
        If Form1.inner3.Image Is Nothing Then
            Form1.inner3.Image = My.Resources.transblank
        End If
        If Form1.inner4.Image Is Nothing Then
            Form1.inner4.Image = My.Resources.transblank
        End If
        If Form1.TL45.Image Is Nothing Then
            Form1.TL45.Image = My.Resources.transblank
        End If
        If Form1.TR45.Image Is Nothing Then
            Form1.TR45.Image = My.Resources.transblank
        End If
        If Form1.BL45.Image Is Nothing Then
            Form1.BL45.Image = My.Resources.transblank
        End If
        If Form1.BR45.Image Is Nothing Then
            Form1.BR45.Image = My.Resources.transblank
        End If
        If Form1.TL26.Image Is Nothing Then
            Form1.TL26.Image = My.Resources.transblank
        End If
        If Form1.TR26.Image Is Nothing Then
            Form1.TR26.Image = My.Resources.transblank
        End If
        If Form1.BL26.Image Is Nothing Then
            Form1.BL26.Image = My.Resources.transblank
        End If
        If Form1.BR26.Image Is Nothing Then
            Form1.BR26.Image = My.Resources.transblank
        End If
        If Form1.TL14.Image Is Nothing Then
            Form1.TL14.Image = My.Resources.transblank
        End If
        If Form1.TR14.Image Is Nothing Then
            Form1.TR14.Image = My.Resources.transblank
        End If
        If Form1.TLINNER14.Image Is Nothing Then
            Form1.TLINNER14.Image = My.Resources.transblank
        End If
        If Form1.TRINNER14.Image Is Nothing Then
            Form1.TRINNER14.Image = My.Resources.transblank
        End If




        Dim BMP1 As New Bitmap(Form1.topleft.Image)
        Dim BMP2 As New Bitmap(Form1.topright.Image)
        Dim BMP3 As New Bitmap(Form1.bottomleft.Image)
        Dim BMP4 As New Bitmap(Form1.bottomright.Image)
        Dim BMP5 As New Bitmap(Form1.inner1.Image)
        Dim BMP6 As New Bitmap(Form1.inner2.Image)
        Dim BMP7 As New Bitmap(Form1.inner3.Image)
        Dim BMP8 As New Bitmap(Form1.inner4.Image)
        Dim BMP9 As New Bitmap(Form1.TL45.Image)
        Dim BMP10 As New Bitmap(Form1.TR45.Image)
        Dim BMP11 As New Bitmap(Form1.BL45.Image)
        Dim BMP12 As New Bitmap(Form1.BR45.Image)
        Dim BMP13 As New Bitmap(Form1.TL26.Image)
        Dim BMP14 As New Bitmap(Form1.TR26.Image)
        Dim BMP15 As New Bitmap(Form1.BL26.Image)
        Dim BMP16 As New Bitmap(Form1.BR26.Image)
        Dim BMP17 As New Bitmap(Form1.TL14.Image)
        Dim BMP18 As New Bitmap(Form1.TR14.Image)
        Dim BMP19 As New Bitmap(Form1.TLINNER14.Image)
        Dim BMP20 As New Bitmap(Form1.TRINNER14.Image)


        If My.Settings.CURRENTSELECTION = "Top-Left" Then
            PictureBox1.Image = BMP1
            PictureBox1.Size = BMP1.Size

            Dim BAMP1 As New Bitmap(BMP1)
            Dim BAMP2 As New Bitmap(BMP1)
            Dim BAMP3 As New Bitmap(BMP1)
            Dim BAMP4 As New Bitmap(BMP1)

            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True

        ElseIf My.Settings.CURRENTSELECTION = "Top-Right" Then
            PictureBox2.Image = BMP2
            PictureBox2.Size = BMP2.Size

            Dim BAMP1 As New Bitmap(BMP2)
            Dim BAMP2 As New Bitmap(BMP2)
            Dim BAMP3 As New Bitmap(BMP2)
            Dim BAMP4 As New Bitmap(BMP2)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipY)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Left" Then
            PictureBox3.Image = BMP3
            PictureBox3.Size = BMP3.Size

            Dim BAMP1 As New Bitmap(BMP3)
            Dim BAMP2 As New Bitmap(BMP3)
            Dim BAMP3 As New Bitmap(BMP3)
            Dim BAMP4 As New Bitmap(BMP3)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Right" Then
            PictureBox4.Image = BMP4
            PictureBox4.Size = BMP4.Size

            Dim BAMP1 As New Bitmap(BMP4)
            Dim BAMP2 As New Bitmap(BMP4)
            Dim BAMP3 As New Bitmap(BMP4)
            Dim BAMP4 As New Bitmap(BMP4)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
        End If


        If My.Settings.CURRENTSELECTION = "Top-Left Inner" Then
            PictureBox1.Image = BMP5
            PictureBox1.Size = BMP5.Size

            Dim BAMP1 As New Bitmap(BMP5)
            Dim BAMP2 As New Bitmap(BMP5)
            Dim BAMP3 As New Bitmap(BMP5)
            Dim BAMP4 As New Bitmap(BMP5)

            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True

        ElseIf My.Settings.CURRENTSELECTION = "Top-Right Inner" Then
            PictureBox2.Image = BMP6
            PictureBox2.Size = BMP6.Size

            Dim BAMP1 As New Bitmap(BMP6)
            Dim BAMP2 As New Bitmap(BMP6)
            Dim BAMP3 As New Bitmap(BMP6)
            Dim BAMP4 As New Bitmap(BMP6)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipY)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True

        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Left Inner" Then
            PictureBox3.Image = BMP7
            PictureBox3.Size = BMP7.Size

            Dim BAMP1 As New Bitmap(BMP7)
            Dim BAMP2 As New Bitmap(BMP7)
            Dim BAMP3 As New Bitmap(BMP7)
            Dim BAMP4 As New Bitmap(BMP7)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "Bottom-Right Inner" Then
            PictureBox4.Image = BMP8
            PictureBox4.Size = BMP8.Size

            Dim BAMP1 As New Bitmap(BMP8)
            Dim BAMP2 As New Bitmap(BMP8)
            Dim BAMP3 As New Bitmap(BMP8)
            Dim BAMP4 As New Bitmap(BMP8)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
        End If


        If My.Settings.CURRENTSELECTION = "45 Slope A" Then
            PictureBox1.Image = BMP9
            PictureBox1.Size = BMP1.Size

            Dim BAMP1 As New Bitmap(BMP9)
            Dim BAMP2 As New Bitmap(BMP9)
            Dim BAMP3 As New Bitmap(BMP9)
            Dim BAMP4 As New Bitmap(BMP9)

            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True

        ElseIf My.Settings.CURRENTSELECTION = "45 Slope B" Then
            PictureBox2.Image = BMP10
            PictureBox2.Size = BMP10.Size

            Dim BAMP1 As New Bitmap(BMP10)
            Dim BAMP2 As New Bitmap(BMP10)
            Dim BAMP3 As New Bitmap(BMP10)
            Dim BAMP4 As New Bitmap(BMP10)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipY)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "45 Slope C" Then
            PictureBox3.Image = BMP11
            PictureBox3.Size = BMP11.Size

            Dim BAMP1 As New Bitmap(BMP11)
            Dim BAMP2 As New Bitmap(BMP11)
            Dim BAMP3 As New Bitmap(BMP11)
            Dim BAMP4 As New Bitmap(BMP11)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "45 Slope D" Then
            PictureBox4.Image = BMP12
            PictureBox4.Size = BMP12.Size

            Dim BAMP1 As New Bitmap(BMP12)
            Dim BAMP2 As New Bitmap(BMP12)
            Dim BAMP3 As New Bitmap(BMP12)
            Dim BAMP4 As New Bitmap(BMP12)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
        End If


        If My.Settings.CURRENTSELECTION = "26 Slope A" Then
            PictureBox1.Image = BMP13
            PictureBox1.Size = BMP13.Size

            Dim BAMP1 As New Bitmap(BMP13)
            Dim BAMP2 As New Bitmap(BMP13)
            Dim BAMP3 As New Bitmap(BMP13)
            Dim BAMP4 As New Bitmap(BMP13)

            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True

        ElseIf My.Settings.CURRENTSELECTION = "26 Slope B" Then
            PictureBox2.Image = BMP14
            PictureBox2.Size = BMP14.Size

            Dim BAMP1 As New Bitmap(BMP14)
            Dim BAMP2 As New Bitmap(BMP14)
            Dim BAMP3 As New Bitmap(BMP14)
            Dim BAMP4 As New Bitmap(BMP14)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipX)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipY)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "26 Slope C" Then
            PictureBox3.Image = BMP15
            PictureBox3.Size = BMP15.Size

            Dim BAMP1 As New Bitmap(BMP15)
            Dim BAMP2 As New Bitmap(BMP15)
            Dim BAMP3 As New Bitmap(BMP15)
            Dim BAMP4 As New Bitmap(BMP15)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP4.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox4.Image = BAMP4
            PictureBox4.Size = BAMP4.Size
            CheckBox4.Enabled = True
        ElseIf My.Settings.CURRENTSELECTION = "26 Slope D" Then
            PictureBox4.Image = BMP16
            PictureBox4.Size = BMP16.Size

            Dim BAMP1 As New Bitmap(BMP16)
            Dim BAMP2 As New Bitmap(BMP16)
            Dim BAMP3 As New Bitmap(BMP16)
            Dim BAMP4 As New Bitmap(BMP16)

            BAMP1.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            BAMP2.RotateFlip(RotateFlipType.RotateNoneFlipY)
            BAMP3.RotateFlip(RotateFlipType.RotateNoneFlipX)
            PictureBox1.Image = BAMP1
            PictureBox1.Size = BAMP1.Size
            CheckBox1.Enabled = True
            PictureBox2.Image = BAMP2
            PictureBox2.Size = BAMP2.Size
            CheckBox2.Enabled = True
            PictureBox3.Image = BAMP3
            PictureBox3.Size = BAMP3.Size
            CheckBox3.Enabled = True
        End If


       





    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        My.Computer.Audio.Play(My.Resources.smw_reserve_item_store, AudioPlayMode.Background)

        If My.Settings.CURRENTSELECTION = "Top-Left" Then
            If CheckBox2.Checked = True Then
                Form1.topright.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.bottomleft.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.bottomright.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "Top-Right" Then
            If CheckBox1.Checked = True Then
                Form1.topleft.Image = PictureBox1.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.bottomleft.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.bottomright.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "Bottom-Left" Then
            If CheckBox1.Checked = True Then
                Form1.topleft.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.topright.Image = PictureBox2.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.bottomright.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "Bottom-Right" Then
            If CheckBox1.Checked = True Then
                Form1.topleft.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.topright.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.bottomleft.Image = PictureBox3.Image
            End If
        End If


        If My.Settings.CURRENTSELECTION = "Top-Left Inner" Then
            If CheckBox2.Checked = True Then
                Form1.inner2.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.inner3.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.inner4.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "Top-Right Inner" Then
            If CheckBox1.Checked = True Then
                Form1.inner1.Image = PictureBox1.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.inner3.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.inner4.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "Bottom-Left Inner" Then
            If CheckBox1.Checked = True Then
                Form1.inner1.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.inner2.Image = PictureBox2.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.inner4.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "Bottom-Right Inner" Then
            If CheckBox1.Checked = True Then
                Form1.inner1.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.inner2.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.inner3.Image = PictureBox3.Image
            End If
        End If
        If My.Settings.CURRENTSELECTION = "45 Slope A" Then
            If CheckBox2.Checked = True Then
                Form1.TR45.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.BL45.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.BR45.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "45 Slope B" Then
            If CheckBox1.Checked = True Then
                Form1.TL45.Image = PictureBox1.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.BL45.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.BR45.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "45 Slope C" Then
            If CheckBox1.Checked = True Then
                Form1.TL45.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.TR45.Image = PictureBox2.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.BR45.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "45 Slope D" Then
            If CheckBox1.Checked = True Then
                Form1.TL45.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.TR45.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.BL45.Image = PictureBox3.Image
            End If
        End If






        If My.Settings.CURRENTSELECTION = "26 Slope A" Then
            If CheckBox2.Checked = True Then
                Form1.TR26.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.BL26.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.BR26.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "26 Slope B" Then
            If CheckBox1.Checked = True Then
                Form1.TL26.Image = PictureBox1.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.BL26.Image = PictureBox3.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.BR26.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "26 Slope C" Then
            If CheckBox1.Checked = True Then
                Form1.TL26.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.TR26.Image = PictureBox2.Image
            End If
            If CheckBox4.Checked = True Then
                Form1.BR26.Image = PictureBox4.Image
            End If
        End If

        If My.Settings.CURRENTSELECTION = "26 Slope D" Then
            If CheckBox1.Checked = True Then
                Form1.TL26.Image = PictureBox1.Image
            End If
            If CheckBox2.Checked = True Then
                Form1.TR26.Image = PictureBox2.Image
            End If
            If CheckBox3.Checked = True Then
                Form1.BL26.Image = PictureBox3.Image
            End If
        End If











        Me.Close()
    End Sub
End Class