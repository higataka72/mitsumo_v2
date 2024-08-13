Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Namespace Biz

    Public Class MTM04
        Inherits BaseBiz

        Public Property Password As String = ""
        Public Property LoginId As String = ""

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="connectionString"></param>
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        ''' <summary>
        ''' バリデーション前処理(Inport用)
        ''' </summary>
        ''' <param name="mtm10r004renkei"></param>
        ''' <returns>List</returns>
        Public Function ValidateBeforeImport(mtm10r004renkei As Models.MTM10R004RENKEI) As List(Of String)
            Dim errorList As New List(Of String)

            If String.IsNullOrWhiteSpace(mtm10r004renkei.MTMR004002) Then
                errorList.Add("価格入力番号が空です")
            End If

            If String.IsNullOrWhiteSpace(mtm10r004renkei.MTMR004002) Then
                errorList.Add("入力元が空です")
            End If

            Return errorList
        End Function
        ''' <summary>
        ''' 仮採番MAX取得
        ''' </summary>
        ''' <returns>List</returns>
        Public Function TemporaryNumbering() As Decimal
            Dim kariNum As Decimal = 0
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection

                    '仮採番（一旦、見積ヘッダーテーブルのMAXから採番する）
                    command.CommandText = "SELECT ISNULL(MAX(HANR006004),0) + 1 AS KARI_NUM " _
                                            + "FROM HAN10R006MITSUMORIH "
                    command.Parameters.Clear()
                    Dim reader_num As SqlDataReader = command.ExecuteReader
                    If reader_num.Read = True Then
                        kariNum = reader_num.Item("KARI_NUM").ToString()
                    End If
                End Using
                'Using productCommand As New SqlCommand
                '    Dim command As New SqlCommand
                '    command.Connection = Me.connection

                '    '仮採番（一旦、見積ヘッダーテーブルのMAXから採番する）
                '    command.CommandText = "SELECT ISNULL(MAX(HANR006004),0) + 1 AS KARI_NUM " _
                '                            + "FROM HAN10R006MITSUMORIH "
                '    command.Parameters.Clear()
                '    Dim reader_num As SqlDataReader = command.ExecuteReader
                '    If reader_num.Read = True Then
                '        kariNum = reader_num.Item("KARI_NUM").ToString()
                '    End If
                'End Using
            Finally
                Me.connection.Close()
            End Try

            Return kariNum
        End Function
        ''' <summary>
        ''' 価格入力名の取得
        ''' </summary>
        ''' <param name="scMTMR003001"></param>
        ''' <returns>String</returns>
        Public Function KakakuNameSearch(scMTMR003001 As Integer) As String
            Dim reMTMR003002 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMR003002 " _
                                        + "FROM MTM10R003JITSUKOU " _
                                        + "WHERE " _
                                        + "MTMR003001 = @MTMR003001 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR003001", scMTMR003001))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMR003002 = reader.Item("MTMR003002").ToString()      '価格入力名
                    End If
                End Using
            Catch ex As Exception
                Return reMTMR003002
            Finally
                Me.connection.Close()
            End Try

            Return reMTMR003002

        End Function
        ''' <summary>
        ''' 商品名１称の取得
        ''' </summary>
        ''' <param name="scHANMA01001"></param>
        ''' <param name="scHANMA01002"></param>
        ''' <param name="scHANMA01003"></param>
        ''' <returns>String</returns>
        Public Function ProductNameSearch(scHANMA01001 As String, scHANMA01002 As String, scHANMA01003 As String) As String
            Dim reHANMA01074 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANMA01074 " _
                                        + "FROM HAN98MA01TANKA " _
                                        + "WHERE " _
                                        + "HANMA01001 = @HANMA01001 " _
                                        + "AND HANMA01002 = @HANMA01002 " _
                                        + "AND HANMA01003 = @HANMA01003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANMA01001", scHANMA01001))
                    command.Parameters.Add(New SqlParameter("@HANMA01002", scHANMA01002))
                    command.Parameters.Add(New SqlParameter("@HANMA01003", scHANMA01003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANMA01074 = reader.Item("HANMA01074").ToString()      '商品名１
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANMA01074

        End Function
        ''' <summary>
        ''' 商品名２称の取得
        ''' </summary>
        ''' <param name="scHANMA01001"></param>
        ''' <param name="scHANMA01002"></param>
        ''' <param name="scHANMA01003"></param>
        ''' <returns>String</returns>
        Public Function ProductName2Search(scHANMA01001 As String, scHANMA01002 As String, scHANMA01003 As String) As String
            Dim reHANMA01075 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANMA01075 " _
                                        + "FROM HAN98MA01TANKA " _
                                        + "WHERE " _
                                        + "HANMA01001 = @HANMA01001 " _
                                        + "AND HANMA01002 = @HANMA01002 " _
                                        + "AND HANMA01003 = @HANMA01003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANMA01001", scHANMA01001))
                    command.Parameters.Add(New SqlParameter("@HANMA01002", scHANMA01002))
                    command.Parameters.Add(New SqlParameter("@HANMA01003", scHANMA01003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANMA01075 = reader.Item("HANMA01075").ToString()      '商品名２
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANMA01075

        End Function
        ''' <summary>
        ''' 手配区分の取得
        ''' </summary>
        ''' <param name="scHANMA01001"></param>
        ''' <param name="scHANMA01002"></param>
        ''' <param name="scHANMA01003"></param>
        ''' <returns>String</returns>
        Public Function ArrangeDivisionSearch(scHANMA01001 As String, scHANMA01002 As String, scHANMA01003 As String) As String
            Dim reHANMA01008 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANMA01008 " _
                                        + "FROM HAN98MA01TANKA " _
                                        + "WHERE " _
                                        + "HANMA01001 = @HANMA01001 " _
                                        + "AND HANMA01002 = @HANMA01002 " _
                                        + "AND HANMA01003 = @HANMA01003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANMA01001", scHANMA01001))
                    command.Parameters.Add(New SqlParameter("@HANMA01002", scHANMA01002))
                    command.Parameters.Add(New SqlParameter("@HANMA01003", scHANMA01003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANMA01008 = reader.Item("HANMA01008").ToString()      '手配区分（優先順位１）
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANMA01008

        End Function
        ''' <summary>
        ''' 支払条件の取得
        ''' </summary>
        ''' <param name="scHANM001003"></param>
        ''' <returns>String</returns>
        Public Function PaymentTermsSearch(scHANM001003 As String) As String
            Dim rePaymentTerms As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "TKI.HANM001A002 " _  '担当者諸口区分
                                        + ",TKI.HANM001A004 " _ '複数入金設定
                                        + ",TKI.HANM001A005 " _ '入金設定切替額
                                        + ",TKI.HANM001027 " _  '締日１
                                        + ",TKI.HANM001030 " _  '入金日１
                                        + ",TKI.HANM001033 " _  '入金サイクル１
                                        + ",TKI.HANM001A012 " _  '入金設定１ 入金率 入金率１
                                        + ",TKI.HANM001A028 " _  '入金設定２ 入金率 入金率１
                                        + ",TKI.HANM001A007 " _  '入金設定１ 入金率 区分
                                        + ",ISNULL(KBN1.HANC003005, '') AS HANC003005_K1 " _  '入金設定１ 入金率 区分名
                                        + ",TKI.HANM001A023 " _  '入金設定２ 入金率 区分
                                        + ",ISNULL(KBN2.HANC003005, '') as HANC003005_K2 " _  '入金設定２ 入金率 区分名
                                        + "FROM HAN10M001TOKUI TKI " _
                                        + "LEFT OUTER JOIN (SELECT HANC003004, HANC003005 FROM HAN10C003TORIHIKI WHERE HANC003001 = 4) KBN1 " _
                                        + "ON KBN1.HANC003004 = TKI.HANM001A007 " _
                                        + "LEFT OUTER JOIN (SELECT HANC003004, HANC003005 FROM HAN10C003TORIHIKI WHERE HANC003001 = 4) KBN2 " _
                                        + "ON KBN2.HANC003004 = TKI.HANM001A023 " _
                                        + "WHERE " _
                                        + "RTRIM(HANM001003) = @HANM001003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANM001003", scHANM001003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        '支払条件のセット
                        If (reader.Item("HANM001A002").ToString() = "1") Then
                            '得意先マスター．担当者諸口区分＝１（諸口）の場合
                            rePaymentTerms = "現金お振込み"
                            Return rePaymentTerms
                        End If


                        '「30日締」を「末締」に変更(2023/09/12 No191)
                        Dim ClosingDate As String = ""
                        If (Not String.IsNullOrEmpty(reader.Item("HANM001027").ToString()) And reader.Item("HANM001027").ToString() = "30") Then
                            ClosingDate = "末締 "
                        Else
                            ClosingDate = reader.Item("HANM001027").ToString() + "日締 "
                        End If

                        If (reader.Item("HANM001A004").ToString() = "0") Then
                            '得意先マスター.複数入金設定＝０（行わない）の場合
                            If (reader.Item("HANM001027").ToString() = "30") And
                                (reader.Item("HANM001030").ToString() = "30") And
                                (reader.Item("HANM001033").ToString() = "0") Then
                                '締日1＝30 かつ 入金日1=30 かつ 入金サイクル1=0（当月入金）の場合
                                rePaymentTerms = "現金"
                                Return rePaymentTerms
                            Else
                                '上記以外の場合(04/15修正)
                                'rePaymentTerms = reader.Item("HANM001027").ToString() + "日締 " + reader.Item("HANC003005_K1").ToString().Replace(" ", "").Replace("　", "")
                                rePaymentTerms = ClosingDate + reader.Item("HANC003005_K1").ToString().Replace(" ", "").Replace("　", "")
                                Return rePaymentTerms
                            End If
                        ElseIf (reader.Item("HANM001A004").ToString() = "1") Then
                            '得意先マスター．複数入金設定＝１（行う）の場合
                            If (reader.Item("HANM001A012").ToString() = reader.Item("HANM001A028").ToString()) Then
                                '入金設定方法１．入金率1＝入金設定方法２．入金率1の場合(04/15修正)
                                'rePaymentTerms = reader.Item("HANM001027").ToString() + "日締 " + reader.Item("HANC003005_K1").ToString().Replace(" ", "").Replace("　", "")
                                rePaymentTerms = ClosingDate + reader.Item("HANC003005_K1").ToString().Replace(" ", "").Replace("　", "")
                                Return rePaymentTerms
                            Else
                                '入金設定方法１．入金率1＜＞入金設定方法２．入金率1の場合(04/15修正)
                                rePaymentTerms = ClosingDate +
                                                reader.Item("HANM001A005").ToString() +
                                                " 以上 " +
                                                reader.Item("HANC003005_K2").ToString().Replace(" ", "").Replace("　", "") +
                                                " 未満 " +
                                                reader.Item("HANC003005_K1").ToString().Replace(" ", "").Replace("　", "")
                                'rePaymentTerms = reader.Item("HANM001027").ToString() +
                                '                                    "日締 " +
                                '                                    reader.Item("HANM001A005").ToString() +
                                '                                    " 以上 " +
                                '                                    reader.Item("HANC003005_K2").ToString().Replace(" ", "").Replace("　", "") +
                                '                                    " 未満 " +
                                '                                    reader.Item("HANC003005_K1").ToString().Replace(" ", "").Replace("　", "")
                                Return rePaymentTerms
                            End If
                        End If
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return rePaymentTerms

        End Function
        ''' <summary>
        ''' 手入力得意先名１の取得
        ''' </summary>
        ''' <param name="scHANM001003"></param>
        ''' <returns>String</returns>
        Public Function Customer1Search(scHANM001003 As String) As String
            Dim reCustomer1 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANM001004 " _  '得意先１
                                        + "FROM HAN10M001TOKUI " _
                                        + "WHERE " _
                                        + "HANM001003 = @HANM001003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANM001003", scHANM001003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reCustomer1 = reader.Item("HANM001004").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reCustomer1

        End Function
        ''' <summary>
        ''' 手入力得意先名２の取得
        ''' </summary>
        ''' <param name="scHANM001003"></param>
        ''' <returns>String</returns>
        Public Function Customer2Search(scHANM001003 As String) As String
            Dim reCustomer2 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANM001005 " _  '得意先２
                                        + "FROM HAN10M001TOKUI " _
                                        + "WHERE " _
                                        + "HANM001003 = @HANM001003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANM001003", scHANM001003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reCustomer2 = reader.Item("HANM001005").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reCustomer2

        End Function
        ''' <summary>
        ''' 運賃の取得
        ''' </summary>
        ''' <param name="scMTMR003001"></param>
        ''' <returns>String</returns>
        Public Function FareSearch(scMTMR003001 As String) As String
            Dim reHANR006A005 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMR003008 " _  '運賃の指定
                                        + "FROM MTM10R003JITSUKOU " _
                                        + "WHERE " _
                                        + "MTMR003001 = @MTMR003001 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR003001", scMTMR003001))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANR006A005 = reader.Item("MTMR003008").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANR006A005

        End Function
        ''' <summary>
        ''' 価格入力データ取得（システム間連携用）
        ''' </summary>
        ''' <param name="searchCondition"></param>
        ''' <param name="sysError"></param>
        ''' <returns>List</returns>
        Public Function GetDataPrice(ByVal searchCondition As MTM02SearchCondition, ByRef sysError As String) As DataTable
            Dim table As New DataTable

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT * " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND (MTMR002032 <> 0 OR MTMR002035 <> 0) " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') "
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.CommandText += " AND MTMR002012 >= @MTMR002012_From "    '担当者コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.CommandText += " AND MTMR002012 <= @MTMR002012_To "      '担当者コード(To)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCode) Then
                        command.CommandText += " AND MTMR002008 = @MTMR002008 "          '営業所コード
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeFrom) Then
                        command.CommandText += " AND MTMR002001 >= @MTMR002001_From "    '得意先コード(From)
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeTo) Then
                        command.CommandText += " AND MTMR002001 <= @MTMR002001_To "      '得意先コード(To)
                    End If
                    command.CommandText += "ORDER BY MTMR002001, MTMR002002, MTMR002003, MTMR002004, MTMR002032"

                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", searchCondition.KakakuNyuuryokuNo))
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002012_From", searchCondition.TantousyaCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TantousyaCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002012_To", searchCondition.TantousyaCodeTo))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.EigyosyoCode) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002008", searchCondition.EigyosyoCode))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeFrom) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002001_From", searchCondition.TokuisakiCodeFrom))
                    End If
                    If Not String.IsNullOrWhiteSpace(searchCondition.TokuisakiCodeTo) Then
                        command.Parameters.Add(New SqlParameter("@MTMR002001_To", searchCondition.TokuisakiCodeTo))
                    End If
                    Dim adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return table
        End Function
        ''' <summary>
        ''' 価格入力データ取得（指定して見積明細行数を取得）
        ''' </summary>
        ''' <param name="scMTMR002080"></param>
        ''' <param name="scMTMR002001"></param>
        ''' <param name="scMTMR002032"></param>
        ''' <returns>String</returns>
        Public Function GetDataPriceSpecify(ByVal scMTMR002080 As String,
                                            ByVal scMTMR002001 As String,
                                            ByVal scMTMR002032 As String,
                                            ByRef sysError As String) As Decimal
            Dim reHANR006018 As Decimal = 0

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT COUNT(*) AS DETILS_COUNT " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') " _
                                            + "AND LTRIM(RTRIM(MTMR002001)) =  @MTMR002001 " _
                                            + "AND LTRIM(RTRIM(MTMR002032)) =  @MTMR002032 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", scMTMR002080))
                    command.Parameters.Add(New SqlParameter("@MTMR002001", scMTMR002001))
                    command.Parameters.Add(New SqlParameter("@MTMR002032", scMTMR002032))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANR006018 = reader.Item("DETILS_COUNT").ToString() '見積明細行数
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANR006018
        End Function
        ''' <summary>
        ''' 見積明細の見積金額の合計を取得
        ''' </summary>
        ''' <param name="scMTMR002080"></param>
        ''' <param name="scMTMR002001"></param>
        ''' <param name="scMTMR002032"></param>
        ''' <returns>String</returns>
        Public Function GetDataEstimatedAmount(ByVal scMTMR002080 As String,
                                            ByVal scMTMR002001 As String,
                                            ByVal scMTMR002032 As String,
                                            ByRef sysError As String) As Decimal
            Dim reHANR006030 As Decimal = 0

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT FORMAT(SUM((MTMR002019 * MTMR002030)),'N4') AS PRICE_TOTAL " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') " _
                                            + "AND LTRIM(RTRIM(MTMR002001)) =  @MTMR002001 " _
                                            + "AND LTRIM(RTRIM(MTMR002032)) =  @MTMR002032 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", scMTMR002080))
                    command.Parameters.Add(New SqlParameter("@MTMR002001", scMTMR002001))
                    command.Parameters.Add(New SqlParameter("@MTMR002032", scMTMR002032))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANR006030 = reader.Item("PRICE_TOTAL").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANR006030
        End Function
        ''' <summary>
        ''' 見積明細(消費税)の見積金額の合計を取得
        ''' </summary>
        ''' <param name="scMTMR002080"></param>
        ''' <param name="scMTMR002001"></param>
        ''' <param name="scMTMR002032"></param>
        ''' <returns>String</returns>
        Public Function GetDataEstimatedAmountTax(ByVal scMTMR002080 As String,
                                                  ByVal scMTMR002001 As String,
                                                  ByVal scMTMR002032 As String,
                                                  ByRef sysError As String) As Decimal
            Dim reHANR006030 As Decimal = 0

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT FORMAT(SUM((MTMR002004 * MTMR002030)),'N4') AS PRICE_TOTAL " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') " _
                                            + "AND LTRIM(RTRIM(MTMR002001)) =  @MTMR002001 " _
                                            + "AND LTRIM(RTRIM(MTMR002032)) =  @MTMR002032 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", scMTMR002080))
                    command.Parameters.Add(New SqlParameter("@MTMR002001", scMTMR002001))
                    command.Parameters.Add(New SqlParameter("@MTMR002032", scMTMR002032))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANR006030 = reader.Item("PRICE_TOTAL").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANR006030
        End Function
        ''' <summary>
        ''' 見積明細の原価金額の合計を取得
        ''' </summary>
        ''' <param name="scMTMR002080"></param>
        ''' <param name="scMTMR002001"></param>
        ''' <param name="scMTMR002032"></param>
        ''' <returns>String</returns>
        Public Function GetDataTotalCost(ByVal scMTMR002080 As String,
                                         ByVal scMTMR002001 As String,
                                         ByVal scMTMR002032 As String,
                                         ByRef sysError As String) As Decimal
            Dim reHANR006036 As Decimal = 0

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT FORMAT(SUM((MTMR002039 * MTMR002033)),'N4') AS TOTAL_COST " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') " _
                                            + "AND LTRIM(RTRIM(MTMR002001)) =  @MTMR002001 " _
                                            + "AND LTRIM(RTRIM(MTMR002032)) =  @MTMR002032 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", scMTMR002080))
                    command.Parameters.Add(New SqlParameter("@MTMR002001", scMTMR002001))
                    command.Parameters.Add(New SqlParameter("@MTMR002032", scMTMR002032))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANR006036 = reader.Item("TOTAL_COST").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANR006036
        End Function
        ''' <summary>
        ''' 見積明細の粗利額の合計を取得
        ''' </summary>
        ''' <param name="scMTMR002080"></param>
        ''' <param name="scMTMR002001"></param>
        ''' <param name="scMTMR002032"></param>
        ''' <returns>String</returns>
        Public Function GetDataGrossProfit(ByVal scMTMR002080 As String,
                                         ByVal scMTMR002001 As String,
                                         ByVal scMTMR002032 As String,
                                         ByRef sysError As String) As Decimal
            Dim reHANR006037 As Decimal = 0

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT FORMAT(SUM((MTMR002019 * MTMR002030)) - SUM((MTMR002039 * MTMR002033)),'N4') AS GROSS_PROFIT " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') " _
                                            + "AND LTRIM(RTRIM(MTMR002001)) =  @MTMR002001 " _
                                            + "AND LTRIM(RTRIM(MTMR002032)) =  @MTMR002032 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", scMTMR002080))
                    command.Parameters.Add(New SqlParameter("@MTMR002001", scMTMR002001))
                    command.Parameters.Add(New SqlParameter("@MTMR002032", scMTMR002032))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reHANR006037 = reader.Item("GROSS_PROFIT").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reHANR006037
        End Function
        ''' <summary>
        ''' ロット商品判定ロジック
        ''' </summary>
        ''' <param name="tblMTM10R001TANKA"></param>
        ''' <returns>String</returns>
        Public Function LotProcessAnswer(tblMTM10R001TANKA As DataTable, scHANMA01001 As String, scHANMA01002 As String, scHANMA01003 As String, scHANMA01004 As String) As Decimal
            Dim rowsLot As DataRow()
            Dim reValue As Decimal = 1  '返却見積数量
            Dim first As Boolean = True
            Dim beforeValue As Decimal = 0 '１つ前のロットの見積数量
            Try
                rowsLot = tblMTM10R001TANKA.Select("MTMR002001 = '" & scHANMA01001 &
                                                   "' and MTMR002002 = '" & scHANMA01002 &
                                                   "' and MTMR002003 = '" & scHANMA01003 & "'",
                                                   "MTMR002004 asc")

                If rowsLot.Count() > 1 Then
                    'ロット商品の場合
                    first = True
                    For Each row As DataRow In rowsLot
                        If (row("MTMR002004").ToString().Trim() = scHANMA01004) Then
                            If (first) Then
                                '１行目ロット
                                reValue = 1
                                Exit For
                            Else
                                '２行目ロット
                                reValue = beforeValue + 1
                                Exit For
                            End If
                        End If
                        first = False
                        If Not Decimal.TryParse(row("MTMR002004").ToString().Trim(), beforeValue) Then
                            beforeValue = 0
                        End If
                    Next
                ElseIf rowsLot.Count() = 1 Then
                    For Each row As DataRow In rowsLot
                        'ロット商品以外の場合
                        If (row("MTMR002019").ToString().Trim() = "0") Or (row("MTMR002019").ToString().Trim() = "0.0000") Then
                            '入数が0の場合は「1」
                            reValue = 1
                        Else
                            '入数が0以外の場合は入数
                            If Not Decimal.TryParse(row("MTMR002019").ToString().Trim(), reValue) Then
                                reValue = 1
                            End If
                        End If
                    Next
                Else
                    reValue = 1
                End If

            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reValue

        End Function
        ''' <summary>
        ''' 消費税率を制御マスタより取得
        ''' </summary>
        ''' <param name="scMTMR002032"></param>
        ''' <returns>String</returns>
        Public Function TaxNewOldSearch(scMTMR002032 As String) As Decimal
            Dim reTax As Decimal = 0.0
            Dim newTaxDate As DateTime
            Dim taxDate As DateTime

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMM001023, MTMM001024, MTMM001025 " _
                                        + "FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        If (DateTime.TryParse(Integer.Parse(reader.Item("MTMM001023").ToString()).ToString("0000/00/00"), newTaxDate) _
                            And (DateTime.TryParse(Integer.Parse(scMTMR002032).ToString("0000/00/00"), taxDate))) Then
                            '比較対象データ
                            If (newTaxDate > taxDate) Then
                                reTax = reader.Item("MTMM001024").ToString() '旧税率
                            ElseIf (newTaxDate <= taxDate) Then
                                reTax = reader.Item("MTMM001025").ToString() '新税率
                            End If
                        End If
                    End If

                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reTax

        End Function
        ''' <summary>
        ''' システム連携処理
        ''' </summary>
        ''' <param name="tableEstimate"></param>
        ''' <param name="tableEstimateDetils"></param>
        ''' <returns>List</returns>
        Public Function SystemLinkage(ByVal tableEstimate As DataTable, ByVal tableEstimateDetils As DataTable) As List(Of String)
            Dim errorList As New List(Of String)

            Try
                Me.connection.Open()

                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction

                        '------------------------
                        '②見積明細テーブルを生成
                        '　消費税データを作成
                        '------------------------
                        Dim bulkcopyEstimateDetils As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                        bulkcopyEstimateDetils.DestinationTableName = "HAN10R007MITSUMORIM"
                        bulkcopyEstimateDetils.WriteToServer(tableEstimateDetils)

                        '----------------------------
                        '③見積ヘッダーテーブルを生成
                        '----------------------------
                        Dim bulkcopyEstimate As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                        bulkcopyEstimate.DestinationTableName = "HAN10R006MITSUMORIH"
                        bulkcopyEstimate.WriteToServer(tableEstimate)

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        errorList.Add(ex.Message)
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 見積データの本採番
        ''' </summary>
        ''' <param name="dummyNumber"></param>
        ''' <param name="tableMTM10R001TANKA"></param>
        ''' <param name="tableHAN10R006MITSUMORIH"></param>
        ''' <param name="tableHAN10R007MITSUMORIM"></param>
        ''' <param name="tableHAN98MA01TANKA"></param>
        ''' <param name="sysError"></param>
        ''' <returns>List</returns>
        Public Function EstimateProduction(ByVal dummyNumber As List(Of Decimal),
                                           ByVal tableMTM10R001TANKA As DataTable,
                                           ByVal tableHAN10R006MITSUMORIH As DataTable,
                                           ByVal tableHAN10R007MITSUMORIM As DataTable,
                                           ByVal tableHAN98MA01TANKA As DataTable,
                                           ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)

            Dim renbanStart As Decimal = 0
            Dim renbanNext As Decimal = 0
            Dim renbanMax As Decimal = 0
            Dim dtToday = DateTime.Now.ToString("yyyyMMdd")
            Dim updateDate As Decimal = 0

            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        '連番管理テーブルをロックして更新
                        command.CommandText = "SELECT HANC021002 " _
                                            + "FROM HAN10C021RENBAN "
                        command.Parameters.Clear()
                        Dim reader As SqlDataReader = command.ExecuteReader
                        If reader.Read = True Then
                            If Not Decimal.TryParse(reader.Item("HANC021002").ToString(), renbanStart) Then
                                errorList.Add("連番管理テーブルから採番できません")
                                Return errorList
                            End If
                        End If
                        If (dummyNumber.Count() > 0) Then
                            renbanMax = renbanStart + dummyNumber.Count()
                        End If
                        reader.Close()

                        '連番管理テーブルを更新
                        command.CommandText = "UPDATE HAN10C021RENBAN SET HANC021002 = @HANC021002 "
                        command.Parameters.Clear()
                        command.Parameters.Add(New SqlParameter("@HANC021002", renbanMax))
                        command.ExecuteNonQuery()

                        Dim rowsMitsumoriM As DataRow()
                        Dim rowsMitsumoriH As DataRow()
                        Dim rowsTanka As DataRow()

                        '仮採番から本採番へ切り替える
                        renbanNext = (renbanStart + 1)
                        For Each listNumber As String In dummyNumber
                            '見積明細テーブルの本採番
                            rowsMitsumoriM = tableHAN10R007MITSUMORIM.Select("HANR007002='" + listNumber + "'")
                            For Each dataRow As DataRow In rowsMitsumoriM
                                dataRow("HANR007002") = renbanNext
                            Next

                            '見積ヘッダーテーブルの本採番
                            rowsMitsumoriH = tableHAN10R006MITSUMORIH.Select("HANR006004='" + listNumber + "'")
                            For Each dataRow As DataRow In rowsMitsumoriH
                                dataRow("HANR006003") = renbanNext
                                dataRow("HANR006004") = renbanNext
                            Next

                            '単価台帳テーブルの本採番
                            If (tableHAN98MA01TANKA.Rows.Count() > 0) Then
                                rowsTanka = tableHAN98MA01TANKA.Select("HANMA01029='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsTanka
                                    dataRow("HANMA01029") = renbanNext
                                    dataRow("HANMA01038") = renbanNext
                                Next
                            End If

                            '本採番よりプラス１
                            renbanNext = renbanNext + 1
                        Next

                        '------------------------
                        '②見積明細テーブルを生成
                        '　消費税データを作成
                        '------------------------
                        Dim bulkcopyEstimateDetils As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                        bulkcopyEstimateDetils.DestinationTableName = "HAN10R007MITSUMORIM"
                        bulkcopyEstimateDetils.WriteToServer(tableHAN10R007MITSUMORIM)

                        '----------------------------
                        '③見積ヘッダーテーブルを生成
                        '----------------------------
                        Dim bulkcopyEstimate As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                        bulkcopyEstimate.DestinationTableName = "HAN10R006MITSUMORIH"
                        bulkcopyEstimate.WriteToServer(tableHAN10R006MITSUMORIH)

                        '-----------------------------
                        '価格入力データの更新(見積連携日、単価台帳連携日、及び連携担当者コード)
                        '-----------------------------
                        rowsTanka = tableMTM10R001TANKA.Select()
                        If (Not Decimal.TryParse(dtToday, updateDate)) Then
                            updateDate = 0
                        End If
                        For Each dataRow As DataRow In rowsTanka
                            command.CommandText = "UPDATE MTM10R002KAKAKU "
                            command.CommandText += "SET MTMR002089 = @MTMR002089 "
                            command.CommandText += ",MTMR002090 = @MTMR002090 "
                            command.CommandText += ",MTMR002091 = @MTMR002091 "
                            command.CommandText += "WHERE "
                            command.CommandText += "MTMR002001 = @MTMR002001 "
                            command.CommandText += "AND MTMR002002 = @MTMR002002 "
                            command.CommandText += "AND MTMR002003 = @MTMR002003 "
                            command.CommandText += "AND MTMR002004 = @MTMR002004"
                            command.Parameters.Clear()
                            command.Parameters.Add(New SqlParameter("@MTMR002089", updateDate))
                            command.Parameters.Add(New SqlParameter("@MTMR002090", updateDate))
                            command.Parameters.Add(New SqlParameter("@MTMR002091", Me.LoginId))
                            command.Parameters.Add(New SqlParameter("@MTMR002001", dataRow("MTMR002001")))
                            command.Parameters.Add(New SqlParameter("@MTMR002002", dataRow("MTMR002002")))
                            command.Parameters.Add(New SqlParameter("@MTMR002003", dataRow("MTMR002003")))
                            command.Parameters.Add(New SqlParameter("@MTMR002004", dataRow("MTMR002004")))
                            command.ExecuteNonQuery()
                        Next

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function

        ''' <summary>
        ''' 見積データの本採番
        ''' </summary>
        ''' <param name="dummyNumber"></param>
        ''' <param name="tableMTM10R001TANKA"></param>
        ''' <param name="tableHAN10R006MITSUMORIH"></param>
        ''' <param name="tableHAN10R007MITSUMORIM"></param>
        ''' <param name="tableHAN98MA01TANKA"></param>
        ''' <param name="sysError"></param>
        ''' <returns>List</returns>
        Public Function EstimateProductions(ByVal dummyNumber As List(Of Decimal),
                                            ByVal tableMTM10R001TANKA As DataTable,
                                            ByVal tableHAN10R006MITSUMORIH As DataTable,
                                            ByVal tableHAN10R007MITSUMORIM As DataTable,
                                            ByVal tableHAN10R030KAKUCHO_H As DataTable,
                                            ByVal tableHAN10R030KAKUCHO_M As DataTable,
                                            ByVal tableHAN98MA01TANKA As DataTable,
                                            ByVal renbanStart As Decimal,
                                            ByVal renbanMax As Decimal,
                                            ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Dim renbanNext As Decimal = 0
            Dim dtToday = DateTime.Now.ToString("yyyyMMdd")
            Dim updateDate As Decimal = 0

            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction
                        Dim rowsMitsumoriM As DataRow()
                        Dim rowsMitsumoriH As DataRow()
                        Dim rowsKakucho_M As DataRow()
                        Dim rowsKakucho_H As DataRow()
                        Dim rowsTanka As DataRow()

                        '仮採番から本採番へ切り替える
                        renbanNext = (renbanStart + 1)
                        '画面の処理のみ対応（No173見積データがTRUEではない場合は採番はしない）
                        If (renbanStart = 0) And (renbanMax = 0) Then
                            '(No173)
                            For Each listNumber As String In dummyNumber
                                '見積明細テーブルの本採番
                                rowsMitsumoriM = tableHAN10R007MITSUMORIM.Select("HANR007002='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsMitsumoriM
                                    dataRow("HANR007002") = 0
                                Next
                                '明細拡張テーブル（明細）の本採番
                                rowsKakucho_M = tableHAN10R030KAKUCHO_M.Select("HANR030004='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsKakucho_M
                                    dataRow("HANR030004") = 0
                                Next

                                '見積ヘッダーテーブルの本採番
                                rowsMitsumoriH = tableHAN10R006MITSUMORIH.Select("HANR006004='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsMitsumoriH
                                    dataRow("HANR006003") = 0
                                    dataRow("HANR006004") = 0
                                Next
                                '明細拡張テーブル（ヘッダー）の本採番
                                rowsKakucho_H = tableHAN10R030KAKUCHO_H.Select("HANR030004='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsKakucho_H
                                    dataRow("HANR030004") = 0
                                    dataRow("HANR030008") = 0
                                Next

                                '単価台帳テーブルの本採番
                                If (tableHAN98MA01TANKA.Rows.Count() > 0) Then
                                    rowsTanka = tableHAN98MA01TANKA.Select("HANMA01029='" + listNumber + "'")
                                    For Each dataRow As DataRow In rowsTanka
                                        dataRow("HANMA01029") = 0
                                        dataRow("HANMA01030") = 0
                                        dataRow("HANMA01038") = 0
                                        dataRow("HANMA01039") = 0
                                    Next
                                End If

                                '本採番よりプラス１
                                renbanNext = renbanNext + 1
                            Next
                        Else
                            For Each listNumber As String In dummyNumber
                                '見積明細テーブルの本採番
                                rowsMitsumoriM = tableHAN10R007MITSUMORIM.Select("HANR007002='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsMitsumoriM
                                    dataRow("HANR007002") = renbanNext
                                Next
                                '明細拡張テーブル（明細）の本採番
                                rowsKakucho_M = tableHAN10R030KAKUCHO_M.Select("HANR030004='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsKakucho_M
                                    dataRow("HANR030004") = renbanNext
                                    dataRow("HANR030008") = renbanNext
                                Next

                                '見積ヘッダーテーブルの本採番
                                rowsMitsumoriH = tableHAN10R006MITSUMORIH.Select("HANR006004='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsMitsumoriH
                                    dataRow("HANR006003") = renbanNext
                                    dataRow("HANR006004") = renbanNext
                                Next
                                '明細拡張テーブル（ヘッダー）の本採番
                                rowsKakucho_H = tableHAN10R030KAKUCHO_H.Select("HANR030004='" + listNumber + "'")
                                For Each dataRow As DataRow In rowsKakucho_H
                                    dataRow("HANR030004") = renbanNext
                                    dataRow("HANR030008") = renbanNext
                                Next

                                '単価台帳テーブルの本採番
                                If (tableHAN98MA01TANKA.Rows.Count() > 0) Then
                                    rowsTanka = tableHAN98MA01TANKA.Select("HANMA01029='" + listNumber + "'")
                                    For Each dataRow As DataRow In rowsTanka
                                        dataRow("HANMA01029") = renbanNext
                                        dataRow("HANMA01038") = renbanNext
                                    Next
                                End If

                                '本採番よりプラス１
                                renbanNext = renbanNext + 1
                            Next
                        End If

                        If (renbanStart <> 0) And (renbanMax <> 0) Then
                            '------------------------
                            '②見積明細テーブルを生成
                            '　消費税データを作成
                            '------------------------
                            'Dim tableHAN10R007MITSUMORIM2 As DataTable = Models.HAN10R007MITSUMORIM.GetDataTable()
                            'Dim icnt As Integer = 1
                            'For Each dataRow As DataRow In tableHAN10R007MITSUMORIM.Select()
                            '    Dim han10r007mitsumorim As New Models.HAN10R007MITSUMORIM
                            '    icnt += 1
                            '    Console.WriteLine("COUNT: " & icnt)
                            '    Console.WriteLine("LINE: " & dataRow("HANR007002") & "," & dataRow("HANR007003") & "," & dataRow("HANR007004"))
                            'Next

                            'Dim bulkcopyEstimateDetils As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                            'bulkcopyEstimateDetils.DestinationTableName = "HAN10R007MITSUMORIM"
                            'bulkcopyEstimateDetils.WriteToServer(tableHAN10R007MITSUMORIM)

                            '----------------------------
                            '③見積ヘッダーテーブルを生成
                            '----------------------------
                            'Dim tableHAN10R006MITSUMORIH2 As DataTable = Models.HAN10R006MITSUMORIH.GetDataTable()
                            'Dim icnt As Integer = 1
                            'For Each dataRow As DataRow In tableHAN10R006MITSUMORIH.Select()
                            '    Dim han10r007mitsumorih As New Models.HAN10R006MITSUMORIH
                            '    icnt += 1
                            '    Console.WriteLine("COUNT: " & icnt)
                            '    Console.WriteLine("LINE: " & dataRow("HANR006001") & "," & dataRow("HANR006002") & "," & dataRow("HANR006003"))
                            'Next

                            'Dim bulkcopyEstimate As New SqlBulkCopy(Me.connection, SqlBulkCopyOptions.KeepIdentity, transaction)
                            'bulkcopyEstimate.DestinationTableName = "HAN10R006MITSUMORIH"
                            'bulkcopyEstimate.WriteToServer(tableHAN10R006MITSUMORIH)

                            '-----------------------------
                            '価格入力データの更新(見積連携日、単価台帳連携日、及び連携担当者コード)
                            '-----------------------------
                            rowsTanka = tableMTM10R001TANKA.Select()
                            If (Not Decimal.TryParse(dtToday, updateDate)) Then
                                updateDate = 0
                            End If
                            For Each dataRow As DataRow In rowsTanka
                                command.CommandText = "UPDATE MTM10R002KAKAKU "
                                command.CommandText += "SET MTMR002089 = @MTMR002089 "
                                command.CommandText += ",MTMR002090 = @MTMR002090 "
                                command.CommandText += ",MTMR002091 = @MTMR002091 "
                                command.CommandText += "WHERE "
                                command.CommandText += "MTMR002001 = @MTMR002001 "
                                command.CommandText += "AND MTMR002002 = @MTMR002002 "
                                command.CommandText += "AND MTMR002003 = @MTMR002003 "
                                command.CommandText += "AND MTMR002004 = @MTMR002004"
                                command.Parameters.Clear()
                                command.Parameters.Add(New SqlParameter("@MTMR002089", updateDate))
                                command.Parameters.Add(New SqlParameter("@MTMR002090", updateDate))
                                command.Parameters.Add(New SqlParameter("@MTMR002091", Me.LoginId))
                                command.Parameters.Add(New SqlParameter("@MTMR002001", dataRow("MTMR002001")))
                                command.Parameters.Add(New SqlParameter("@MTMR002002", dataRow("MTMR002002")))
                                command.Parameters.Add(New SqlParameter("@MTMR002003", dataRow("MTMR002003")))
                                command.Parameters.Add(New SqlParameter("@MTMR002004", dataRow("MTMR002004")))
                                command.ExecuteNonQuery()
                            Next
                        End If

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' スクリプト作成
        ''' </summary>
        ''' <param name="fileDirectory"></param>
        ''' <param name="tableHAN10R006MITSUMORIH"></param>
        ''' <param name="tableHAN10R007MITSUMORIM"></param>
        ''' <param name="tableHAN98MA01TANKA"></param>
        ''' <param name="tableHAN98MA02SUTANKA"></param>
        ''' <param name="sysError"></param>
        ''' <returns>List</returns>
        Public Function SqlScriptOutput(ByVal fileDirectory As String,
                                        ByVal kakakuNo As String,
                                        ByVal tableHAN10R006MITSUMORIH As DataTable,
                                        ByVal tableHAN10R007MITSUMORIM As DataTable,
                                        ByVal tableHAN10R030KAKUCHO_H As DataTable,
                                        ByVal tableHAN10R030KAKUCHO_M As DataTable,
                                        ByVal tableHAN98MA01TANKA As DataTable,
                                        ByVal tableHAN98MA02SUTANKA As DataTable,
                                        ByRef sysError As String,
                                        ByVal outputTanka As Boolean,
                                        ByVal outputMitsumori As Boolean) As List(Of String)
            Dim errorList As New List(Of String)

            Dim insertHAN10R006MITSUMORIH As String
            Dim fileNameHAN10R007MITSUMORIH As String = "MITSUMORIH_INS"
            Dim insertHAN10R007MITSUMORIM As String
            Dim fileNameHAN10R007MITSUMORIM As String = "MITSUMORIM_INS"
            '明細拡張テーブル（ヘッダ、明細）04/17
            Dim inserHAN10R030KAKUCHO_H As String
            Dim fileNameHAN10R030KAKUCHO_H As String = "KAKUCHO_HEADER_INS"
            Dim inserHAN10R030KAKUCHO_M As String
            Dim fileNameHAN10R030KAKUCHO_M As String = "KAKUCHO_DETAIL_INS"
            Dim updateHAN98MA01TANKA As String
            Dim fileNameHAN98MA01TANKA As String = "TANKA_UPD"
            Dim fileNameHAN98MA01TANKA_ERR As String = "TANKA_UPD_"
            Dim updateHAN98MA02SUTANKA As String
            Dim fileNameHAN98MA02SUTANKA As String = "SUTANKA_UPD"
            Dim fileNameHAN98MA02SUTANKA_ERR As String = "SUTANKA_UPD_ERR"
            Dim dtToday = DateTime.Now.ToString("yyyyMMdd")
            Dim captureToday = DateTime.Now                   '今日日付
            'Dim captureNo = captureToday.ToString("hhmmssfff")

            'ディレクトリチェック
            ' 指定したフォルダがあるかどうか確認する
            If Not Directory.Exists(fileDirectory) Then
                ' 指定したフォルダ名を作成する
                Directory.CreateDirectory(fileDirectory)
            End If

            Try
                If (outputMitsumori) Then
                    '見積ヘッダースクリプト生成
                    Dim sw1 As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN10R007MITSUMORIH + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    For Each dataRow As DataRow In tableHAN10R006MITSUMORIH.Rows
                        insertHAN10R006MITSUMORIH = "INSERT INTO HAN10R006MITSUMORIH VALUES ( "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006001") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006002") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006003") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006004") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006005") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006006") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006007") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006008") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006009") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006010") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006011") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006012") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006013") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006014") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006015") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006016") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006017") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006018") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006019") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006020") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006021") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006022") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006023") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006024") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006025") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006026") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006027") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006028") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006029") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006030") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006031") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006032") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006033") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006034") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006035") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006036") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006037") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006038") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006039") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006040") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006041") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006042") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006043") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006044") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006045") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006046") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006047") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006999") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006048") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006049") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006050") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006051") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006052") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006053") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006911") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006914") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006915") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006916") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006917") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006918") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006919") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006INS") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006UPD") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006054") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006055") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006056") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006057") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006058") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006059") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006060") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006061") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006062") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006063") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006064") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006065") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006066") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006067") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A001") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A002") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A003") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A004") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A005") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A006") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A007") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A008") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A009") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A010") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A011") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A012") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A013") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A014") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A015") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A016") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A017") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A018") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A019") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A020") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A021") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A022") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A023") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A024") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A025") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A026") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A027") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A028") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A029") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A030") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A031") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A032") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A033") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A034") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A035") + ", "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A036") + ", "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A037") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A038") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A039") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A040") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A041") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A042") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A043") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A044") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A045") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A046") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A047") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A048") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A049") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A050") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A051") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A052") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A053") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A054") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A055") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A056") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A057") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A058") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A059") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A060") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A061") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A062") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A063") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A064") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A065") + "', "
                        insertHAN10R006MITSUMORIH += "'" + dataRow("HANR006A066") + "', "
                        insertHAN10R006MITSUMORIH += dataRow("HANR006A067")
                        insertHAN10R006MITSUMORIH += ");"
                        sw1.Write(insertHAN10R006MITSUMORIH)
                        sw1.Write(vbCrLf)
                    Next
                    sw1.Close()

                    '明細拡張テーブル（ヘッダー）スクリプト生成
                    Dim sw1_2 As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN10R030KAKUCHO_H + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    For Each dataRow As DataRow In tableHAN10R030KAKUCHO_H.Rows
                        inserHAN10R030KAKUCHO_H = "INSERT INTO HAN10R030KAKUCHO VALUES ( "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030001") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030002") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030003") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030004") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030005") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030006") + ", "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030007") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030008") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030009") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030010") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030011") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030012") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030013") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030014") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030015") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030016") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030017") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030018") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030019") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030020") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030021") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030022") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030023") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030024") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030025") + "', "
                        inserHAN10R030KAKUCHO_H += "'" + dataRow("HANR030026") + "', "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030999") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030INS") + ", "
                        inserHAN10R030KAKUCHO_H += dataRow("HANR030UPD")
                        inserHAN10R030KAKUCHO_H += ");"
                        sw1_2.Write(inserHAN10R030KAKUCHO_H)
                        sw1_2.Write(vbCrLf)
                    Next
                    sw1_2.Close()

                    '見積明細テーブル用のスクリプト生成
                    Dim sw2 As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN10R007MITSUMORIM + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    For Each dataRow As DataRow In tableHAN10R007MITSUMORIM.Rows
                        insertHAN10R007MITSUMORIM = "INSERT INTO HAN10R007MITSUMORIM VALUES ( "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007001") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007002") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007003") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007004") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007005") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007006") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007007") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007008") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007009") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007010") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007011") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007012") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007013") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007014") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007015") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007016") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007017") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007018") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007019") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007020") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007021") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007022") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007023") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007024") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007025") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007026") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007027") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007028") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007029") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007030") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007031") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007032") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007033") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007034") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007035") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007036") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007037") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007038") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007039") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007040") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007041") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007042") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007999") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007043") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007044") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007045") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007046") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007047") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007048") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007049") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007050") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007051") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007052") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007INS") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007UPD") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007053") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007054") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007055") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007056") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007057") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007058") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007059") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007060") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007061") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007062") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007063") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A001") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A002") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A003") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A004") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A005") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A006") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A007") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A008") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A009") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A010") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A011") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A012") + "', "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A013") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A014") + ", "
                        insertHAN10R007MITSUMORIM += dataRow("HANR007A015") + ", "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A016") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A017") + "', "
                        insertHAN10R007MITSUMORIM += "'" + dataRow("HANR007A018") + "'"
                        insertHAN10R007MITSUMORIM += ");"
                        sw2.Write(insertHAN10R007MITSUMORIM)
                        sw2.Write(vbCrLf)
                    Next
                    sw2.Close()

                    '明細拡張テーブル（明細）スクリプト生成
                    Dim sw2_2 As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN10R030KAKUCHO_M + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    For Each dataRow As DataRow In tableHAN10R030KAKUCHO_M.Rows
                        inserHAN10R030KAKUCHO_M = "INSERT INTO HAN10R030KAKUCHO VALUES ( "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030001") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030002") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030003") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030004") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030005") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030006") + ", "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030007") + "', "
                        If (dataRow("HANR030005") = "0") Then
                            inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030008") + "', "
                        Else
                            inserHAN10R030KAKUCHO_M += " '', "
                        End If
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030009") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030010") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030011") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030012") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030013") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030014") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030015") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030016") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030017") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030018") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030019") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030020") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030021") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030022") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030023") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030024") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030025") + "', "
                        inserHAN10R030KAKUCHO_M += "'" + dataRow("HANR030026") + "', "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030999") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030INS") + ", "
                        inserHAN10R030KAKUCHO_M += dataRow("HANR030UPD")
                        inserHAN10R030KAKUCHO_M += ");"
                        sw2_2.Write(inserHAN10R030KAKUCHO_M)
                        sw2_2.Write(vbCrLf)
                    Next
                    sw2_2.Close()

                End If

                If (outputTanka) Then
                    '数量別単価台帳スクリプト生成
                    Dim sw3 As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA02SUTANKA + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    Dim sw3err As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA02SUTANKA_ERR + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    Dim sw3Rows As Decimal = 0
                    Dim sw3errRows As Decimal = 0
                    For Each dataRow As DataRow In tableHAN98MA02SUTANKA.Rows
                        If DataCheckSutanka(dataRow("HANMA02001"), dataRow("HANMA02002"), dataRow("HANMA02003"), dataRow("HANMA02005")) Then
                            sw3Rows += 1
                            updateHAN98MA02SUTANKA = "UPDATE  HAN98MA02SUTANKA SET "
                            If (Not dataRow("HANMA02009").ToString() = "0") Then
                                updateHAN98MA02SUTANKA += "HANMA02009 = " + dataRow("HANMA02009") + ","
                            End If
                            If (Not dataRow("HANMA02010").ToString() = "0") Then
                                updateHAN98MA02SUTANKA += "HANMA02010 = " + dataRow("HANMA02010") + ","
                            End If
                            If (Not dataRow("HANMA02009").ToString() = 0) Then
                                updateHAN98MA02SUTANKA += "HANMA02011 = " + dataRow("HANMA02011") + ","
                            End If
                            If (Not dataRow("HANMA02010").ToString() = 0) Then
                                updateHAN98MA02SUTANKA += "HANMA02012 = " + dataRow("HANMA02012") + " "
                            End If
                            updateHAN98MA02SUTANKA += "WHERE "
                            updateHAN98MA02SUTANKA += "HANMA02001 = '" + dataRow("HANMA02001") + "' "
                            updateHAN98MA02SUTANKA += "AND HANMA02002 = '" + dataRow("HANMA02002") + "' "
                            updateHAN98MA02SUTANKA += "AND HANMA02003 = '" + dataRow("HANMA02003") + "' "
                            updateHAN98MA02SUTANKA += "AND HANMA02004 = 0 "
                            updateHAN98MA02SUTANKA += "AND HANMA02005 = " + dataRow("HANMA02005") + " "
                            updateHAN98MA02SUTANKA += "AND HANMA02006 = " + dataRow("HANMA02006")
                            sw3.Write(updateHAN98MA02SUTANKA)
                            sw3.Write(vbCrLf)
                        Else
                            sw3errRows += 1
                            updateHAN98MA02SUTANKA = "UPDATE  HAN98MA02SUTANKA SET "
                            If (Not dataRow("HANMA02009").ToString() = "0") Then
                                updateHAN98MA02SUTANKA += "HANMA02009 = " + dataRow("HANMA02009") + ","
                            End If
                            If (Not dataRow("HANMA02010").ToString() = "0") Then
                                updateHAN98MA02SUTANKA += "HANMA02010 = " + dataRow("HANMA02010") + ","
                            End If
                            If (Not dataRow("HANMA02009").ToString() = 0) Then
                                updateHAN98MA02SUTANKA += "HANMA02011 = " + dataRow("HANMA02011") + ","
                            End If
                            If (Not dataRow("HANMA02010").ToString() = 0) Then
                                updateHAN98MA02SUTANKA += "HANMA02012 = " + dataRow("HANMA02012") + " "
                            End If
                            updateHAN98MA02SUTANKA += "WHERE "
                            updateHAN98MA02SUTANKA += "HANMA02001 = '" + dataRow("HANMA02001") + "' "
                            updateHAN98MA02SUTANKA += "AND HANMA02002 = '" + dataRow("HANMA02002") + "' "
                            updateHAN98MA02SUTANKA += "AND HANMA02003 = '" + dataRow("HANMA02003") + "' "
                            updateHAN98MA02SUTANKA += "AND HANMA02004 = 0 "
                            updateHAN98MA02SUTANKA += "AND HANMA02005 = " + dataRow("HANMA02005") + " "
                            updateHAN98MA02SUTANKA += "AND HANMA02006 = " + dataRow("HANMA02006")
                            sw3err.Write(updateHAN98MA02SUTANKA)
                            sw3err.Write(vbCrLf)
                        End If
                    Next
                    sw3.Close()
                    sw3err.Close()
                    'ゴミファイル削除
                    If (sw3Rows = 0) Then
                        File.Delete(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA02SUTANKA + "_" + dtToday + ".sql")
                    End If
                    If (sw3errRows = 0) Then
                        File.Delete(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA02SUTANKA_ERR + "_" + dtToday + ".sql")
                    End If

                    '単価台帳スクリプト生成
                    Dim sw4 As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA01TANKA + "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                    Dim sw4Rows As Decimal = 0
                    Dim sw4errRows As Decimal = 0
                    For Each dataRow As DataRow In tableHAN98MA01TANKA.Rows
                        If DataCheckTanka(dataRow("HANMA01001"), dataRow("HANMA01002"), dataRow("HANMA01003")) Then
                            sw4Rows += 1
                            updateHAN98MA01TANKA = "UPDATE  HAN98MA01TANKA SET "
                            If (Not dataRow("HANMA01020").ToString() = "0") Then
                                updateHAN98MA01TANKA += "HANMA01020 = " + dataRow("HANMA01020") + ","
                            End If
                            If (Not dataRow("HANMA01021").ToString() = "0") Then
                                updateHAN98MA01TANKA += "HANMA01021 = " + dataRow("HANMA01021") + ","
                            End If
                            updateHAN98MA01TANKA += "HANMA01029 = " + dataRow("HANMA01029") + ","
                            updateHAN98MA01TANKA += "HANMA01030 = " + dataRow("HANMA01030") + ","
                            updateHAN98MA01TANKA += "HANMA01034 = 'BULK UPDATE',"
                            updateHAN98MA01TANKA += "HANMA01035 = " + dataRow("HANMA01035") + "000000,"
                            updateHAN98MA01TANKA += "HANMA01036 = '" + dataRow("HANMA01036") + "' "
                            If (dataRow("HANMA01029") = 0) Then
                                updateHAN98MA01TANKA += ",HANMA01038 = " + dataRow("HANMA01038") + " "
                                updateHAN98MA01TANKA += ",HANMA01039 = " + dataRow("HANMA01039") + " "
                                updateHAN98MA01TANKA += ",HANMA01UPD = " + dataRow("HANMA01UPD") + " "
                            End If
                            updateHAN98MA01TANKA += "WHERE "
                            updateHAN98MA01TANKA += "HANMA01001 = '" + dataRow("HANMA01001") + "' "
                            updateHAN98MA01TANKA += "AND HANMA01002 = '" + dataRow("HANMA01002") + "' "
                            updateHAN98MA01TANKA += "AND HANMA01003 = '" + dataRow("HANMA01003") + "' "
                            sw4.Write(updateHAN98MA01TANKA)
                            sw4.Write(vbCrLf)
                        Else
                            Dim sw4err As StreamWriter = New StreamWriter(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA01TANKA_ERR +
                                                                      "_" + dataRow("HANMA01001") +
                                                                      "_" + dataRow("HANMA01002") +
                                                                      "_" + dataRow("HANMA01003") +
                                                                      "_" + dtToday + ".sql", False, System.Text.Encoding.Default)
                            sw4errRows += 1
                            updateHAN98MA01TANKA = "UPDATE  HAN98MA01TANKA SET "
                            If (Not dataRow("HANMA01020").ToString() = "0") Then
                                updateHAN98MA01TANKA += "HANMA01020 = " + dataRow("HANMA01020") + ","
                            End If
                            If (Not dataRow("HANMA01021").ToString() = "0") Then
                                updateHAN98MA01TANKA += "HANMA01021 = " + dataRow("HANMA01021") + ","
                            End If
                            updateHAN98MA01TANKA += "HANMA01029 = " + dataRow("HANMA01029") + ","
                            updateHAN98MA01TANKA += "HANMA01030 = " + dataRow("HANMA01030") + ","
                            updateHAN98MA01TANKA += "HANMA01034 = 'BULK UPDATE',"
                            updateHAN98MA01TANKA += "HANMA01035 = " + dataRow("HANMA01035") + "000000,"
                            updateHAN98MA01TANKA += "HANMA01036 = '" + dataRow("HANMA01036") + "' "
                            If (dataRow("HANMA01029") = 0) Then
                                updateHAN98MA01TANKA += ",HANMA01038 = " + dataRow("HANMA01038") + " "
                                updateHAN98MA01TANKA += ",HANMA01039 = " + dataRow("HANMA01039") + " "
                                updateHAN98MA01TANKA += ",HANMA01UPD = " + dataRow("HANMA01UPD") + " "
                            End If
                            updateHAN98MA01TANKA += "WHERE "
                            updateHAN98MA01TANKA += "HANMA01001 = '" + dataRow("HANMA01001") + "' "
                            updateHAN98MA01TANKA += "AND HANMA01002 = '" + dataRow("HANMA01002") + "' "
                            updateHAN98MA01TANKA += "AND HANMA01003 = '" + dataRow("HANMA01003") + "' "
                            sw4err.Write(updateHAN98MA01TANKA)
                            sw4err.Write(vbCrLf)
                            sw4err.Close()
                        End If
                    Next
                    sw4.Close()
                    'ゴミファイル削除
                    If (sw4Rows = 0) Then
                        File.Delete(fileDirectory + "\" + kakakuNo + "_" + fileNameHAN98MA01TANKA + "_" + dtToday + ".sql")
                    End If
                End If
            Catch ex As Exception
                errorList.Add("スクリプトの作成に失敗しました")
                errorList.Add(ex.Message())
                Return errorList
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' 数量別単価台帳マスター存在チェック
        ''' <param name="scHANMA02001"></param>
        ''' <param name="scHANMA02002"></param>
        ''' <param name="scHANMA02003"></param>
        ''' <param name="scHANMA02005"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataCheckSutanka(ByVal scHANMA02001 As String, ByVal scHANMA02002 As String, ByVal scHANMA02003 As String, ByVal scHANMA02005 As Decimal) As Boolean
            Dim reBool As Boolean = False
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANMA02001 " _
                                        + "FROM HAN98MA02SUTANKA " _
                                        + "WHERE LTRIM(RTRIM(HANMA02001)) = @HANMA02001 " _
                                            + "AND LTRIM(RTRIM(HANMA02002)) =  @HANMA02002 " _
                                            + "AND LTRIM(RTRIM(HANMA02003)) =  @HANMA02003 " _
                                            + "AND HANMA02004 =  0 " _
                                            + "AND HANMA02005 =  @HANMA02005 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANMA02001", scHANMA02001))
                    command.Parameters.Add(New SqlParameter("@HANMA02002", scHANMA02002))
                    command.Parameters.Add(New SqlParameter("@HANMA02003", scHANMA02003))
                    command.Parameters.Add(New SqlParameter("@HANMA02005", scHANMA02005))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reBool = True
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reBool

        End Function
        ''' <summary>
        ''' 単価台帳マスター存在チェック
        ''' <param name="scHANMA01001"></param>
        ''' <param name="scHANMA01002"></param>
        ''' <param name="scHANMA01003"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataCheckTanka(ByVal scHANMA01001 As String, ByVal scHANMA01002 As String, ByVal scHANMA01003 As String) As Boolean
            Dim reBool As Boolean = False
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "HANMA01001 " _
                                        + "FROM HAN98MA01TANKA " _
                                        + "WHERE LTRIM(RTRIM(HANMA01001)) = @HANMA01001 " _
                                            + "AND LTRIM(RTRIM(HANMA01002)) =  @HANMA01002 " _
                                            + "AND LTRIM(RTRIM(HANMA01003)) =  @HANMA01003 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@HANMA01001", scHANMA01001))
                    command.Parameters.Add(New SqlParameter("@HANMA01002", scHANMA01002))
                    command.Parameters.Add(New SqlParameter("@HANMA01003", scHANMA01003))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reBool = True
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reBool

        End Function
        ''' <summary>
        ''' 制御マスタのシステム間連携フォルダ
        ''' </summary>
        ''' <returns>String</returns>
        Public Function FileDirectory() As String
            Dim reMTMM001014 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMM001014 " _  'システム間連携フォルダ
                                        + "FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMM001014 = reader.Item("MTMM001014").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reMTMM001014

        End Function

        ''' <summary>
        ''' 制御マスタの価格入力番号
        ''' </summary>
        ''' <returns>String</returns>
        Public Function GetKakakuNumber() As String
            Dim reMTMM001001 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "MTMM001003 " _  '価格入力番号
                                        + "FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMM001001 = reader.Item("MTMM001003").ToString()
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reMTMM001001

        End Function
        ''' <summary>
        ''' 制御マスタのシステム間連携日付から起動日判定
        ''' </summary>
        ''' <returns>String</returns>
        Public Function GetAppStartDate() As Boolean
            Dim reBool As Boolean = False
            Dim dtJudgment As String
            Dim dtToday = DateTime.Now.ToString("yyyyMMdd")

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT " _
                                        + "RTRIM(MTMM001013) AS MTMM001013 " _  'システム間連携日付
                                        + "FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        dtJudgment = reader.Item("MTMM001013").ToString
                        dtJudgment = dtJudgment.Substring(0, 8)
                        If (dtToday = dtJudgment) Then reBool = True
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reBool

        End Function

        ''' <summary>
        ''' システム間連携フォルダを取得（制御マスタ）
        ''' </summary>
        ''' <returns>String</returns>
        Public Function OutputFileNameSearch() As String
            Dim reMTMM001011 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM001014 FROM MTM00M001SEIGYO "
                    command.Parameters.Clear()
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMM001011 = reader.Item("MTMM001014").ToString()      'システム間連携フォルダ
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return reMTMM001011
        End Function
        ''' <summary>
        ''' 価格入力番号取得
        ''' </summary>
        ''' <returns>String</returns>
        Public Function GetCostInputNumber() As String
            Dim number As String = ""

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMM001003 FROM MTM00M001SEIGYO WHERE MTMM001002 = @MTMM001002"
                    command.Parameters.Add(New SqlParameter("@MTMM001002", Me.Password))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        number = reader.Item("MTMM001003").ToString()
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return number
        End Function
        ''' <summary>
        ''' 価格入力番号、名称を取得
        ''' </summary>
        ''' <returns>String</returns>
        Public Function GetKakakuName(ByVal scMTMR003001 As Decimal) As String
            Dim reMTMR003002 As String = ""

            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMR003002 FROM MTM10R003JITSUKOU WHERE MTMR003001 = @MTMR003001"
                    command.Parameters.Add(New SqlParameter("@MTMR003001", scMTMR003001))

                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMR003002 = reader.Item("MTMR003002").ToString()
                    End If
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return reMTMR003002
        End Function

        ''' <summary>
        ''' 制御マスタ更新（出力先フォルダ）
        ''' <param name="scMTMM001014"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataUpdateSeigyo(ByVal scMTMM001014 As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "UPDATE MTM00M001SEIGYO " _
                                        + "SET MTMM001014 = @MTMM001014 "
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMM001014", scMTMM001014))
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return errorList

        End Function

        ''' <summary>
        ''' 連携管理テーブルへ登録
        ''' <param name="mtm10r004renkei"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function DataInsertRenkei(ByVal mtm10r004renkei As Models.MTM10R004RENKEI, ByVal tableHAN10R006MITSUMORIH As DataTable, ByRef sysError As String) As List(Of String)
            Dim errorList As New List(Of String)
            Try
                Me.connection.Open()
                Using transaction As SqlTransaction = Me.connection.BeginTransaction
                    Try
                        Dim command As New SqlCommand
                        command.Connection = Me.connection
                        command.Transaction = transaction

                        For Each dataRow As DataRow In tableHAN10R006MITSUMORIH.Rows
                            command.CommandText = "INSERT INTO MTM10R004RENKEI (" _
                            + "  MTMR004001" _ '連携番号
                            + ", MTMR004002" _ '価格入力番号
                            + ", MTMR004003" _ '価格入力名
                            + ", MTMR004004" _ '出力対象
                            + ", MTMR004005" _ '担当者コード　開始
                            + ", MTMR004006" _ '担当者コード　終了
                            + ", MTMR004007" _ '営業所コード
                            + ", MTMR004008" _ '得意先コード　開始
                            + ", MTMR004009" _ '得意先コード　終了
                            + ", MTMR004010" _ '出力年月日
                            + ", MTMR004011" _ '出力フォルダ名
                            + ", MTMR004012" _ '単価台帳
                            + ", MTMR004013" _ '見積書
                            + ", MTMR004014" _ '取込開始時間
                            + ", MTMR004015" _ '取込終了時間
                            + ") VALUES (" _
                            + "  @MTMR004001" _
                            + ", @MTMR004002" _
                            + ", @MTMR004003" _
                            + ", @MTMR004004" _
                            + ", @MTMR004005" _
                            + ", @MTMR004006" _
                            + ", @MTMR004007" _
                            + ", @MTMR004008" _
                            + ", @MTMR004009" _
                            + ", @MTMR004010" _
                            + ", @MTMR004011" _
                            + ", @MTMR004012" _
                            + ", @MTMR004013" _
                            + ", @MTMR004014" _
                            + ", @MTMR004015" _
                            + ")"
                            command.Parameters.Clear()
                            Dim decMTMR004001 As Decimal
                            If (Not Decimal.TryParse(dataRow("HANR006004"), decMTMR004001)) Then
                                Throw New Exception("MTMR004001 number error")
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR004001", decMTMR004001))
                            Dim decMTMR004002 As Decimal
                            If (Not Decimal.TryParse(mtm10r004renkei.MTMR004002, decMTMR004002)) Then
                                Throw New Exception("MTMR004002 number error")
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR004002", decMTMR004002))
                            command.Parameters.Add(New SqlParameter("@MTMR004003", mtm10r004renkei.MTMR004003))
                            Dim decMTMR004004 As Decimal
                            If (Not Decimal.TryParse(mtm10r004renkei.MTMR004004, decMTMR004004)) Then
                                decMTMR004004 = 0
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR004004", decMTMR004004))
                            command.Parameters.Add(New SqlParameter("@MTMR004005", mtm10r004renkei.MTMR004005))
                            command.Parameters.Add(New SqlParameter("@MTMR004006", mtm10r004renkei.MTMR004006))
                            Dim decMTMR004007 As Decimal
                            If (Not Decimal.TryParse(mtm10r004renkei.MTMR004007, decMTMR004007)) Then
                                decMTMR004007 = 0
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR004007", decMTMR004007))
                            command.Parameters.Add(New SqlParameter("@MTMR004008", mtm10r004renkei.MTMR004008))
                            command.Parameters.Add(New SqlParameter("@MTMR004009", mtm10r004renkei.MTMR004009))
                            command.Parameters.Add(New SqlParameter("@MTMR004010", mtm10r004renkei.MTMR004010))
                            command.Parameters.Add(New SqlParameter("@MTMR004011", mtm10r004renkei.MTMR004011))
                            Dim decMTMR004012 As Decimal
                            If (Not Decimal.TryParse(mtm10r004renkei.MTMR004012, decMTMR004012)) Then
                                Throw New Exception("MTMR004012 number error")
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR004012", decMTMR004012))
                            Dim decMTMR004013 As Decimal
                            If (Not Decimal.TryParse(mtm10r004renkei.MTMR004013, decMTMR004013)) Then
                                Throw New Exception("MTMR004013 number error")
                            End If
                            command.Parameters.Add(New SqlParameter("@MTMR004013", decMTMR004013))
                            command.Parameters.Add(New SqlParameter("@MTMR004014", mtm10r004renkei.MTMR004014))
                            command.Parameters.Add(New SqlParameter("@MTMR004015", mtm10r004renkei.MTMR004015))
                            command.ExecuteNonQuery()
                        Next

                        transaction.Commit()

                    Catch ex As Exception
                        transaction.Rollback()
                        sysError = ex.Message
                        Throw ex
                    End Try
                End Using
            Finally
                Me.connection.Close()
            End Try

            Return errorList
        End Function
        ''' <summary>
        ''' フォーマット関数(Decimal桁数指定)
        ''' <param name="number"></param>
        ''' <param name="maxNumOfDigits"></param>
        ''' <param name="numOfDecimalPlaces"></param>
        ''' </summary>
        ''' <returns>Boolean</returns>
        Public Function FormatDecimalNumber(number As Decimal, maxNumOfDigits As Integer, numOfDecimalPlaces As Integer) As String
            Dim ret = String.Empty
            Dim n = numOfDecimalPlaces
            Do While n >= 0
                ret = number.ToString($"F{n}")
                If ret.Length <= maxNumOfDigits Then Exit Do
                n -= 1
            Loop
            If n < 0 Then ret = New String("#"c, maxNumOfDigits)
            Return ret
        End Function

        ''' <summary>
        ''' 納品先履歴の変換
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <returns>String</returns>
        Public Function DeliveryConvert(checkTarget As String) As String
            Dim reString As String = ""
            Dim strConvert As String
            Dim LenB As Integer = 0

            '半角かなコンバート
            If (Not String.IsNullOrEmpty(checkTarget)) Then
                strConvert = StrConv(checkTarget, VbStrConv.Narrow)
                '100バイト変換
                reString = CutStrByteLen2(strConvert, 36)
                'reString = CutStrByteLen(strConvert, 100)
            End If

            Return reString

        End Function
        ''' <summary>
        ''' 文字列バイト数変換
        ''' </summary>
        ''' <param name="strInput"></param>
        ''' <param name="intLen"></param>
        ''' <returns>String</returns>
        Public Function CutStrByteLen(ByVal strInput As String, ByVal intLen As Integer) As String
            Dim sjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim tempLen As Integer = sjis.GetByteCount(strInput)
            ' 引数チェック
            If intLen < 0 OrElse strInput.Length <= 0 Then
                Return ""
            End If
            ' 文字列が指定のバイト数未満の場合は、入力をそのまま返す
            If tempLen <= intLen Then
                Return strInput
            End If
            Dim bytTemp As Byte() = sjis.GetBytes(strInput)
            Dim strTemp As String = sjis.GetString(bytTemp, 0, intLen) & Space(1) & "他"
            If strTemp.EndsWith(ControlChars.NullChar) OrElse strTemp.EndsWith("・") Then
                strTemp = sjis.GetString(bytTemp, 0, intLen - 1) + " 他"
            End If
            Return strTemp
        End Function

        ''' <summary>
        ''' 文字列バイト数変換
        ''' </summary>
        ''' <param name="strInput"></param>
        ''' <param name="intLen"></param>
        ''' <returns>String</returns>
        Public Function CutStrByteLen2(ByVal strInput As String, ByVal intLen As Integer) As String
            Dim InputString As String
            Dim Pos As Integer
            Dim Len As Integer
            Dim lenSum As Integer
            Dim sb As New Text.StringBuilder

            InputString = strInput

            For Pos = 0 To InputString.Length - 1
                Len = Encoding.GetEncoding("Shift_JIS").GetByteCount(InputString.Substring(Pos, 1))
                lenSum += Len
                If lenSum <= intLen Then
                    sb.Append(InputString.Substring(Pos, 1))
                End If
            Next

            Return sb.ToString
        End Function

        ''' <summary>
        ''' 数値チェック桁数(整数部、小数部）チェック
        ''' 小数部がない場合はcheckDigit2=0を指定
        ''' </summary>
        ''' <param name="checkTarget"></param>
        ''' <param name="checkDigit1"></param>
        ''' <param name="checkDigit2"></param>
        ''' <returns>Boolean</returns>
        Public Function NumericNumberDigitsCheck(checkTarget As String, checkDigit1 As Integer, checkDigit2 As Integer) As Boolean
            Dim check As Boolean = False
            Dim getDot As Integer = 0
            Dim getIntegerLength As Integer = 0
            Dim getDecimalLength As Integer = 0

            '数値チェック
            If IsNumeric(checkTarget) Then
                getIntegerLength = checkTarget.Length
                getDot = checkTarget.IndexOf(".")
                If (getDot = -1) Then
                    '整数部の桁数チェックのみ
                    If (getIntegerLength <= checkDigit1) Then
                        check = True
                    End If
                Else
                    '整数部の桁数取得
                    getIntegerLength = checkTarget.Substring(0, getDot).Length
                    '小数部の桁数
                    getDecimalLength = checkTarget.Substring(getDot + 1).Length

                    '整数部、小数部の桁数チェック
                    If (getIntegerLength <= checkDigit1) And ((getDecimalLength > 0) And (getDecimalLength <= checkDigit2)) Then
                        check = True
                    End If
                End If
            End If

            Return check

        End Function

        ''' <summary>
        ''' 宛先名を取得
        ''' </summary>
        ''' <param name="scMTMR002080"></param>
        ''' <param name="scMTMR002001"></param>
        ''' <returns>String</returns>
        Public Function AddressNameSearch(scMTMR002080 As String, scMTMR002001 As String) As String
            Dim reMTMR002079 As String = ""
            Try
                Me.connection.Open()
                Using command As New SqlCommand
                    command.Connection = Me.connection
                    command.CommandText = "SELECT MTMR002079 " _
                                            + "FROM MTM10R002KAKAKU " _
                                            + "WHERE MTMR002080 = @MTMR002080 " _
                                            + "AND MTMR002001 = @MTMR002001 " _
                                            + "AND (MTMR002032 <> 0 OR MTMR002035 <> 0) " _
                                            + "AND (LTRIM(RTRIM(MTMR002085)) != '' AND MTMR002085 IS NOT NULL AND MTMR002085 != '0') "
                    command.CommandText += "ORDER BY MTMR002001, MTMR002002, MTMR002003, MTMR002004, MTMR002032"
                    command.Parameters.Clear()
                    command.Parameters.Add(New SqlParameter("@MTMR002080", scMTMR002080))
                    command.Parameters.Add(New SqlParameter("@MTMR002001", scMTMR002001))
                    Dim reader As SqlDataReader = command.ExecuteReader
                    If reader.Read = True Then
                        reMTMR002079 = reader.Item("MTMR002079").ToString()      '宛先名
                    End If
                End Using
            Catch ex As Exception
                Throw ex
            Finally
                Me.connection.Close()
            End Try

            Return reMTMR002079

        End Function

    End Class

    Public Class MTM04SearchCondition
        ''' <summary>
        ''' 価格入力番号
        ''' </summary>
        ''' <returns></returns>
        Public Property KakakuNyuuryokuNo As String = ""
        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property EigyosyoCode As String = ""
        ''' <summary>
        ''' 担当者コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property TantousyaCodeFrom As String = ""
        ''' <summary>
        ''' 担当者コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property TantousyaCodeTo As String = ""
        ''' <summary>
        ''' 得意先コード(From)
        ''' </summary>
        ''' <returns></returns>
        Public Property TokuisakiCodeFrom As String = ""
        ''' <summary>
        ''' 得意先コード(To)
        ''' </summary>
        ''' <returns></returns>
        Public Property TokuisakiCodeTo As String = ""

        Public Sub Clear()
            Me.KakakuNyuuryokuNo = ""
            Me.EigyosyoCode = ""
            Me.TantousyaCodeFrom = ""
            Me.TantousyaCodeTo = ""
            Me.TokuisakiCodeFrom = ""
            Me.TokuisakiCodeTo = ""
        End Sub
    End Class
End Namespace
