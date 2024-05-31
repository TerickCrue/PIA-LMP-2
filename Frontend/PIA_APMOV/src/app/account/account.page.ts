import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ModalController, NavController } from '@ionic/angular';
import { UsuarioService } from '../services/http/usuario.service';
import { UsuarioDto } from '../interface/usuario-dto';
import { AuthService } from '../auth/auth.service';
import { EditarInformacionComponent } from './editar-informacion/editar-informacion.component';


@Component({
  selector: 'app-account',
  templateUrl: './account.page.html',
  styleUrls: ['./account.page.scss'],
})
export class AccountPage implements OnInit {
  menuType: string = 'overlay';
  protected usuario = new UsuarioDto;

  constructor(
    private route: ActivatedRoute,
    private usarioService: UsuarioService,
    private authService: AuthService,
    private modalControler: ModalController

  ) { }


  async ngOnInit() {
    this.consultarUsuario();
    //await this.storage.create();

    //this.nombreUsuario = await this.storage.get('user');

  }

  protected consultarUsuario(){
    this.usarioService.consultarUsuarioSesion().subscribe({
      next: (data) => {
        console.log(data);
        this.usuario = data;
      }
    })
  }

  protected cerrarSesion(){
    this.authService.cerrarSesion();
  }

  protected async editarInformacion(){
    const modal = await this.modalControler.create({
      component: EditarInformacionComponent,
    })

    await modal.present();

    const data = await modal.onWillDismiss();
    
    if(data.role == "confirm"){
      this.consultarUsuario();
    }
  }
}
