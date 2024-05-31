import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Storage } from '@ionic/storage-angular';
import { AlertController } from '@ionic/angular';
import { NavController } from '@ionic/angular';
import { LoginRequest } from 'src/app/interface/login-request';
import { LoginService } from '../../services/http/login.service';
import { lastValueFrom } from 'rxjs';
import { LoginResponse } from 'src/app/interface/login-response';
import { AuthService } from 'src/app/auth/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  // data = {
  //   user: "",
  //   password: ""
  // };

  protected loginRequest = new LoginRequest;
  protected btnSubmit: boolean = false;

  constructor(
    //private storage: Storage,
    private alertController: AlertController,
    private navCtrl: NavController,
    private loginService: LoginService,
    private authService: AuthService,
    private router: Router
  ) { }

  async ngOnInit() {
    //await this.storage.create();
  }

  // async loginUser() {
  //   const user = await this.storage.get('user');
  //   const password = await this.storage.get('password');

  //   if (user === this.data.user && password === this.data.password) {
  //     this.goToHome();
  //   } else {
  //     this.succesfullAlert();
  //   }
  // }

  // async goToHome() {
  //   //const user = await this.storage.get('user');
  //   //this.navCtrl.navigateForward('/account', { queryParams: { userName: user } });
  //   this.router.navigateByUrl("/recompensas")
  // }

  // async succesfullAlert() {
  //   const alert = await this.alertController.create({
  //     header: 'Error',
  //     subHeader: '',
  //     message: 'El Usuario no existe o su contraseña es incorrecta.',
  //     buttons: ['OK'],
  //   });

  //   await alert.present();
  // }

  public async autenticarUsuario() {
    this.btnSubmit = true;

    await lastValueFrom(this.loginService.authenticate(this.loginRequest))
          .then((loginResponse: LoginResponse) => {
            this.authService.guardarToken(loginResponse.token);
            this.router.navigate(['/recompensas']);
          })
          .catch(error => {
            this.authService.cerrarSesion(false);
            this.btnSubmit = false;
          });
  }

}
