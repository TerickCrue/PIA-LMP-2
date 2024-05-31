import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { UsuarioDto } from 'src/app/interface/usuario-dto';
import { UsuarioService } from 'src/app/services/http/usuario.service';

@Component({
  selector: 'app-editar-informacion',
  templateUrl: './editar-informacion.component.html',
  styleUrls: ['./editar-informacion.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    FormsModule,
    CommonModule
  ]
})
export class EditarInformacionComponent  implements OnInit {

  protected usuario = new UsuarioDto;
  protected submitting: boolean = false;

  constructor(
    private usuarioService: UsuarioService,
    private modalController: ModalController,
    private alertController: AlertController
  ) { }

  ngOnInit() {
    this.consultarUsuario();
  }

  protected consultarUsuario(){
    this.usuarioService.consultarUsuarioSesion().subscribe({
      next: (data)=> {
        console.log(data);
        this.usuario = data;

      }
    })
  }

  protected enviarFormulario(formulario: NgForm){
    this.submitting = true;
    if(formulario.valid){
      this.actualizarInformacion();
    }
  }

  private actualizarInformacion(){
    this.usuarioService.actualizarInformacionUsuario(this.usuario).subscribe({
      next: (data)=> {
        console.log(data);
      },
      error: () => {
        this.submitting = false;
      },
      complete: ()=> {
        this.submitting = false;
        this.presentAlertEditado();
      }
    })
  }

  protected cerrar(rol: string){
    this.modalController.dismiss(null, rol);
  }

  protected async presentAlertEditado(){
    const alert = await this.alertController.create({
      header: "Información actualizada",
      message: "La información se actualizó exitosamente",
      buttons: [{
        text: 'Ok',
        handler: () => {
          this.cerrar('confirm');
        }
      }]
    })

    await alert.present();
  }
}
