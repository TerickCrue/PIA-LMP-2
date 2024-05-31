import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Storage } from '@ionic/storage-angular';
import { AlertController } from '@ionic/angular';
import { NuevoUsuarioDto } from 'src/app/interface/nuevo-usuario-dto';
import { UsuarioService } from 'src/app/services/http/usuario.service';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {

  protected usuarioNuevo = new NuevoUsuarioDto;
  protected submitting: boolean = false;
  protected confirmarContrasena: string = "";

  constructor(
    private router: Router,
    private storage: Storage,
    private alertController: AlertController,
    private usuarioService: UsuarioService,
  ) { }

  async ngOnInit() {
    //await this.storage.create();
  }


  protected enviarFormulario(formulario: NgForm){

    this.submitting = true;

    if(this.usuarioNuevo.contrasena !== this.confirmarContrasena){
      this.submitting = false;
      this.errorAlert('Ups!', 'Las contraseñas no coinciden');
      return;
    }

    if(formulario.valid){
      this.agregarUsuario(this.usuarioNuevo);
    }
  }

  private agregarUsuario(usuarioNuevo: NuevoUsuarioDto){
    this.usuarioService.agregarNuevoUsuario(usuarioNuevo).subscribe({
      next: (data) => {
        console.log(data);
      },
      error: () => {
        this.submitting = false;
        this.errorAlert('Ups!', 'Ingrese correctamente los datos.' );
      },
      complete: () => {
        this.submitting = false;
        this.succesfullAlert()
      }
    })
  }

  // registerUser() {
  //   if (this.data.user == "" || this.data.password == "") {
  //     this.errorAlert();
  //   }
  //   else {
  //     this.storage.set("user", this.data.user).then((val) => {
  //       this.storage.set("password", this.data.password).then((val) => {
  //         this.succesfullAlert();
  //         this.goToLogin();
  //       })
  //     })
  //   }

  // }

  async succesfullAlert() {
    const alert = await this.alertController.create({
      header: 'Enhorabuena!',
      subHeader: '',
      message: 'Usuario Creado Exitosamente!',
      buttons: [{
        text: 'Ok',
        role: 'confirm',
        handler: ()=> {
          this.goToLogin();
        }
      }],
    });

    await alert.present();
  }

  async errorAlert(header: string, message: string) {
    const alert = await this.alertController.create({
      header: header,
      message: message,
      buttons: ['OK'],
    });

    await alert.present();
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }
}
