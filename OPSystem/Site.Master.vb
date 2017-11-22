Imports Microsoft.AspNet.Identity
Imports Security_System
Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Private Ds As New DataSet
    Private oUsr As UserLogin
    Protected Friend sInfo As String = String.Empty
    Protected Sub Page_Init(sender As Object, e As EventArgs)
        oUsr = Session("Usr")
        If Not oUsr Is Nothing Then
            If Request.RawUrl.Split("/").Length > 0 Then
                sInfo = Request.RawUrl.Split("/")(Request.RawUrl.Split("/").Length - 1)
                If sInfo <> "" Then
                    Dim sPageAspx As String = Request.RawUrl.Split("/")(Request.RawUrl.Split("/").Length - 1)
                    If InStr(sPageAspx, "?") > 0 Then sPageAspx = sPageAspx.Split("?")(0)
                    Dim sDirWeb As String = Request.RawUrl.Split("/")(Request.RawUrl.Split("/").Length - 2)
                    sInfo = GetFunction(sDirWeb + "/" + sPageAspx, "SGFuncion")
                End If
            End If
        End If

        ' El código siguiente ayuda a proteger frente a ataques XSRF
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Utilizar el token Anti-XSRF de la cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generar un nuevo token Anti-XSRF y guardarlo en la cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                 .HttpOnly = True,
                 .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Establecer token Anti-XSRF
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Validar el token Anti-XSRF
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Error de validación del token Anti-XSRF.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'lblAplicación.Text = Session("SYS")
        If Not oUsr Is Nothing Then
            LoadMenu()
            Menu.Visible = True
        Else
            Menu.Visible = False
        End If
    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Session.Remove("Usr")
        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    End Sub

    'Ernesto Alvarez Flores
    Private Sub LoadMenu()
        Dim oSQL As New AppMenu(oUsr)
        Dim oCs As New ColeccionPrmSql
        Try

            If Not IsPostBack Then
                oCs.Create("@keypfl", oUsr.Pfl.ID)
                If oSQL.GetQry(Ds, "MENUITEMS", oSQL.Lista, oCs) Then
                    For Each drMenuItem As DataRow In Ds.Tables("MENUITEMS").Rows
                        'Condición que indica si es elemento padre
                        If drMenuItem("men_keymen").ToString.Equals(drMenuItem("men_keypad")) Then
                            Dim mnuMenuItem As New MenuItem
                            mnuMenuItem.Value = drMenuItem("men_keymen").ToString
                            mnuMenuItem.Text = drMenuItem("men_desmen").ToString
                            'mnuMenuItem.ImageUrl = oUsr.ImagenItem
                            'Agregamos el Item al menu
                            Menu.Items.Add(mnuMenuItem)
                            'Llamada al metodo recursivo
                            AddMenuItem(mnuMenuItem, Ds.Tables("MENUITEMS"))
                        End If
                    Next

                End If

            End If

        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex)
        End Try
    End Sub

    Private Sub AddMenuItem(ByRef mnuMenuItem As MenuItem, ByVal dtMenuItems As DataTable)
        'Recorremos cada elemento del datatable para determinar los elementos hijos
        'del menuitem pasado como parametro Byref.

        For Each drMenuItem As DataRow In dtMenuItems.Rows
            If drMenuItem("men_keypad").ToString.Equals(mnuMenuItem.Value) AndAlso
               Not drMenuItem("men_keymen").ToString.Equals(drMenuItem("men_keypad")) Then
                Dim mnuNewMenuItem As New MenuItem
                mnuNewMenuItem.Value = drMenuItem("men_keymen").ToString
                mnuNewMenuItem.Text = drMenuItem("men_desmen").ToString
                'mnuMenuItem.ImageUrl = 
                If drMenuItem("men_fmaspx").ToString <> "" Then
                    Dim s = drMenuItem("men_fmaspx").ToString
                    'mnuNewMenuItem.NavigateUrl = s & ".aspx"
                    mnuNewMenuItem.NavigateUrl = s & IIf(InStr(s, ".aspx") > 0, "", ".aspx")
                End If
                'Agregamos el nuevo MenuItem al MenuItem de nivel superior
                mnuMenuItem.ChildItems.Add(mnuNewMenuItem)

                'Llamada recursiva para ver si el nuevo menu item tiene elementos hijos
                AddMenuItem(mnuNewMenuItem, dtMenuItems)

            End If
        Next

    End Sub
    Private Function GetFunction(ByVal sPage As String, ByVal sCampo As String) As String
        Dim oSql As New AppFunciones(oUsr)
        Dim oCs As New ColeccionPrmSql
        GetFunction = String.Empty
        Try
            oCs.Create("@SGFuncionWeb", sPage)
            Return oSql._Value(oSql.ItemForPage, sCampo, oCs)
        Catch ex As Exception
            Tools.AddErrorLog(oUsr.Mis.Log, ex.Message)
        End Try

    End Function

End Class