Public Class Form2
    Dim _time10, infiniteCounter As Integer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim LIS As New InterfaceLIS
        'LIS.SendOrderToLIS("201612/0021", "2016120100021")

    End Sub

   
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        _time10 += 1
        If _time10 = 10 Then
            'GVPendingOrderBind()
            'GVStatusOrderBind()
            'GVPendingResultBind()
            'GVStatusResultBind()
            'GVPendigRepeatBind()
            'GVStatusRepeatBind()
            'GVPendingDeleteBind()
            'GVStatusDeleteBind()
            'GVErrorBind()
            'GV24Bind()
            'GV24ErrorBind()
            'GVStatus24Bind()
            _time10 = 0
        End If

        If infiniteCounter = 2 Then
            Label1.Text = "Connect Fail"
            Label1.ForeColor = Drawing.Color.Red
            'clientSocket.Close()
        End If
        'ส่ง ข้อมูลจนหมด Row
        '1 ส่ง Delete
        '2 ส่ง Repeat
        '3 ส่ง Result
        '4 ส่ง Order

        'ส่ง Delete
        'If dt_PendingDelete_Row <> 0 Then
        '    SendHIS("NDelete")
        'Else
        ''ส่ง Repeat Result
        'If dt_PendingRepeat_Row <> 0 Then
        '    SendHIS("NRepeat")
        'Else
        '    'ส่งผลเกิน 24 ชม.
        '    If dt_24_Row <> 0 Then
        '        SendHIS("N24")
        '    Else
        '        'ส่ง Pendig Order
        '        If dt_PendingOrder_Row <> 0 Then
        '            SendHIS("NOrder")
        '        Else
        '            'ส่ง  Result
        '            If dt_PendingResult_Row <> 0 Then
        '                SendHIS("NResult")
        '            Else
        '                'ส่ง Error
        '                If dt_Error_Row <> 0 Then
        '                    SendHIS("SendError")
        '                Else
        '                    If dt_24Error_Row <> 0 Then
        '                        SendHIS("Send24Error")
        '                    End If
        '                End If

        '            End If
        '        End If
        '    End If
        'End If
        'End If

    End Sub
End Class