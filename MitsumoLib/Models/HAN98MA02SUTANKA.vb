Namespace Models
    Public Class HAN98MA02SUTANKA
        ''' <summary>
        ''' 得意先コード
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02001 As String
        ''' <summary>
        ''' 商品コード
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02002 As String
        ''' <summary>
        ''' 規格
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02003 As String
        ''' <summary>
        ''' 台帳データ区分
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02004 As Decimal
        ''' <summary>
        ''' 行番号
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02005 As Decimal
        ''' <summary>
        ''' 数量・上限
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02006 As Decimal
        ''' <summary>
        ''' 売上単価
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02007 As Decimal
        ''' <summary>
        ''' 仕入単価
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02008 As Decimal
        ''' <summary>
        ''' 単価変更日付（売上）
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02009 As Decimal
        ''' <summary>
        ''' 単価変更日付（仕入）
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02010 As Decimal
        ''' <summary>
        ''' 売上単価（新）
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02011 As Decimal
        ''' <summary>
        ''' 仕入単価（新）
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02012 As Decimal
        ''' <summary>
        ''' 更新番号
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02999 As Decimal
        ''' <summary>
        ''' 登録日時
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02INS As Decimal
        ''' <summary>
        ''' 更新日時
        ''' </summary>
        ''' <returns></returns>
        Public Property HANMA02UPD As Decimal

        Public Shared Function GetDataTable() As DataTable
            Dim dt As New DataTable
            Dim hanma02001 As New DataColumn("HANMA02001", Type.GetType("System.String"))
            Dim hanma02002 As New DataColumn("HANMA02002", Type.GetType("System.String"))
            Dim hanma02003 As New DataColumn("HANMA02003", Type.GetType("System.String"))
            Dim hanma02004 As New DataColumn("HANMA02004", Type.GetType("System.String"))
            Dim hanma02005 As New DataColumn("HANMA02005", Type.GetType("System.String"))
            Dim hanma02006 As New DataColumn("HANMA02006", Type.GetType("System.String"))
            Dim hanma02007 As New DataColumn("HANMA02007", Type.GetType("System.String"))
            Dim hanma02008 As New DataColumn("HANMA02008", Type.GetType("System.String"))
            Dim hanma02009 As New DataColumn("HANMA02009", Type.GetType("System.String"))
            Dim hanma02010 As New DataColumn("HANMA02010", Type.GetType("System.String"))
            Dim hanma02011 As New DataColumn("HANMA02011", Type.GetType("System.String"))
            Dim hanma02012 As New DataColumn("HANMA02012", Type.GetType("System.String"))
            Dim hanma02999 As New DataColumn("HANMA02999", Type.GetType("System.String"))
            Dim hanma02ins As New DataColumn("HANMA02INS", Type.GetType("System.String"))
            Dim hanma02upd As New DataColumn("HANMA02UPD", Type.GetType("System.String"))
            dt.Columns.Add(hanma02001)
            dt.Columns.Add(hanma02002)
            dt.Columns.Add(hanma02003)
            dt.Columns.Add(hanma02004)
            dt.Columns.Add(hanma02005)
            dt.Columns.Add(hanma02006)
            dt.Columns.Add(hanma02007)
            dt.Columns.Add(hanma02008)
            dt.Columns.Add(hanma02009)
            dt.Columns.Add(hanma02010)
            dt.Columns.Add(hanma02011)
            dt.Columns.Add(hanma02012)
            dt.Columns.Add(hanma02999)
            dt.Columns.Add(hanma02ins)
            dt.Columns.Add(hanma02upd)
            Return dt
        End Function
        Public Sub SetDataRow(ByVal datatable As DataTable)
            Dim dr As DataRow = datatable.NewRow()
            dr.BeginEdit()
            dr("HANMA02001") = Me.HANMA02001
            dr("HANMA02002") = Me.HANMA02002
            dr("HANMA02003") = Me.HANMA02003
            dr("HANMA02004") = Me.HANMA02004
            dr("HANMA02005") = Me.HANMA02005
            dr("HANMA02006") = Me.HANMA02006
            dr("HANMA02007") = Me.HANMA02007
            dr("HANMA02008") = Me.HANMA02008
            dr("HANMA02009") = Me.HANMA02009
            dr("HANMA02010") = Me.HANMA02010
            dr("HANMA02011") = Me.HANMA02011
            dr("HANMA02012") = Me.HANMA02012
            dr("HANMA02999") = Me.HANMA02999
            dr("HANMA02INS") = Me.HANMA02INS
            dr("HANMA02UPD") = Me.HANMA02UPD
            dr.EndEdit()
            datatable.Rows.Add(dr)
        End Sub

    End Class
End Namespace
