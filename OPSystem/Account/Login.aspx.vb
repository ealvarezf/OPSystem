Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin
Imports Security_System

Partial Public Class Login
    Inherits Page
    Dim oUsr As New UserLogin

    Private Sub Login_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Ernesto Alvarez Flores
        With oUsr
            .Mis.Log = Server.MapPath("~/") + "App_Data\Err.log"
            .Mis.Status = "A"
        End With
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        RegisterHyperLink.NavigateUrl = "Register"
        ' Habilite esta opción una vez tenga la confirmación de la cuenta habilitada para la funcionalidad de restablecimiento de contraseña
        ' ForgotPasswordHyperLink.NavigateUrl = "Forgot"
        OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If Not [String].IsNullOrEmpty(returnUrl) Then
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then
            ' Linea por Ernesto para el caso de no haber elección de aplicación
            If oUsr.Mis.StrCnx = "" Then oUsr.Mis.StrCnx = CnxDefault.GetDataStoreInfoEncript
            ' Validar la contraseña del usuario
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

            ' Esto no cuenta los errores de inicio de sesión hacia el bloqueo de cuenta
            ' Para habilitar los errores de contraseña para desencadenar el bloqueo, cambie a shouldLockout := True
            Dim result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout:=False)

            Select Case result
                Case SignInStatus.Success
                    oUsr.Email = Email.Text
                    If CnxDefault.GetUserInfo(oUsr) Then Session.Add("Usr", oUsr)
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                    Exit Select
                Case SignInStatus.LockedOut
                    Response.Redirect("/Account/Lockout")
                    Exit Select
                Case SignInStatus.RequiresVerification
                    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                    Request.QueryString("ReturnUrl"),
                                                    RememberMe.Checked),
                                      True)
                    Exit Select
                Case Else
                    FailureText.Text = "Intento de inicio de sesión no válido"
                    ErrorMessage.Visible = True
                    Exit Select
            End Select
        End If
    End Sub

End Class
