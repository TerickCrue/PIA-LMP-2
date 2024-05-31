import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavController } from '@ionic/angular';
import { MascotaPerdidaService } from '../services/http/mascota-perdida.service';


@Component({
  selector: 'app-recompensas',
  templateUrl: './recompensas.page.html',
  styleUrls: ['./recompensas.page.scss'],
})
export class RecompensasPage implements OnInit {

  constructor(private router: Router, private navCtrl: NavController, private mascotaPerdidaService: MascotaPerdidaService) { }
  menuType: string = 'overlay';
  mascotas: any[] = [];


  ngOnInit() {
    //this.mascotas = JSON.parse(localStorage.getItem("Mascotas") || "[]");
    this.consultarMascotas();
    
  }


  gotoMapa() {
    this.navCtrl.navigateForward('/map-aviso');
  }
  gotoRecompensas() {
    this.navCtrl.navigateForward('/recompensas');
  }

  consultarMascotas(){
    this.mascotaPerdidaService.consultarMascotas().subscribe({
      next: (data) => {
        console.log(data);
        this.mascotas = data;
      }
    })
  }
}
