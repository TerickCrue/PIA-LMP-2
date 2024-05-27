import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AlertController, IonModal, Platform } from '@ionic/angular';
import { Router } from '@angular/router';
import { NavController } from '@ionic/angular';
import { Storage } from '@ionic/storage-angular';
import { MascotaPerdida } from '../interface/mascota-perdida';
import { MascotaPerdidaService } from '../services/mascota-perdida.service';

declare var google: any;

@Component({
  selector: 'app-map-aviso',
  templateUrl: './map-aviso.page.html',
  styleUrls: ['./map-aviso.page.scss'],
})
export class MapAvisoPage implements OnInit {
  @ViewChild('map', { static: false }) mapElement!: ElementRef;
  @ViewChild(IonModal) modal!: IonModal;

  map: any;

  constructor(
    private platform: Platform, 
    private router: Router, 
    private navCtrl: NavController, 
    private storage: Storage,
    private mascotaPerdidaService: MascotaPerdidaService,
    private alertController: AlertController
  ) { }
  menuType: string = 'overlay';

  marker: any;
  direccion: any;
  mascotas: any[] = [];
  mostrarMensajeUbicacion: boolean = false;
  isOpen: boolean = false;
  fechaSeleccionada = new Date().toISOString();

  protected mascota: MascotaPerdida = {
    nombre: undefined,
    descripcion: undefined,
    fecha: new Date(),
    latitud: undefined,
    longitud: undefined,
    recompensa: undefined,
  };

  svgMarker = {
    path: "M240,108a28,28,0,1,1-28-28A28.1,28.1,0,0,1,240,108ZM72,108a28,28,0,1,0-28,28A28.1,28.1,0,0,0,72,108ZM92,88A28,28,0,1,0,64,60,28.1,28.1,0,0,0,92,88Zm72,0a28,28,0,1,0-28-28A28.1,28.1,0,0,0,164,88Zm23.1,60.8a35.3,35.3,0,0,1-16.9-21.1,43.9,43.9,0,0,0-84.4,0A35.5,35.5,0,0,1,69,148.8,40,40,0,0,0,88,224a40.5,40.5,0,0,0,15.5-3.1,64.2,64.2,0,0,1,48.9-.1A39.6,39.6,0,0,0,168,224a40,40,0,0,0,19.1-75.2Z",
    fillColor: "red",
    fillOpacity: 1,
    strokeWeight: 2,
    rotation: 0,
    scale: 0.1,
    anchor: new google.maps.Point(0, 20),
  };

  ngOnInit() {
    //this.mascotas = JSON.parse(localStorage.getItem("Mascotas") || "[]");
    this.platform.ready().then(() => {
      this.initMap();
      console.log(this.mascota);
    });

  }

  initMap() {
    const mapOptions = {
      center: new google.maps.LatLng(25.727878, -100.313096),
      zoom: 13,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
    this.map.addListener("click", (event: any) => {
      
      const latitud = event.latLng.toJSON().lat;
      const longitud = event.latLng.toJSON().lng;
      this.mascota.latitud = latitud.toString();
      this.mascota.longitud = longitud.toString();
      this.direccion = event.latLng;
      console.log(this.mascota);
      this.addMarker();      
    });
    this.marker = new google.maps.Marker({
      map: this.map,
      icon: this.svgMarker
    });

  }

  addMarker() {
    this.marker.setPosition(this.direccion);
  }

  agregarMascota() {
    if(!this.mascota.latitud && !this.mascota.longitud){
      this.mostrarMensajeUbicacion= true;
      return;
    }
    if(this.mascota.nombre && this.mascota.recompensa && this.mascota.descripcion){
      this.mascota.fecha = new Date(this.fechaSeleccionada);
      this.mascota.recompensa = this.mascota.recompensa?.toString();
      console.log(this.mascota);


      this.mascotaPerdidaService.agregarMascota(this.mascota).subscribe({
        complete: () => {
          this.mascota = new MascotaPerdida;
          this.presentarAlerta();
        }
      })
    }
    
    // this.mascotas.push(this.mascota);
    // localStorage.setItem('Mascotas', JSON.stringify(this.mascotas));
    // this.mascotas = JSON.parse(localStorage.getItem("Mascotas") || "[]");
  }

  close(){
    this.modal.dismiss()
  }

  setOpen(opcion: boolean){
    this.isOpen = opcion;
    this.mostrarMensajeUbicacion = false;
  }

  navigateMascotasPerdidas(){
    this.modal.dismiss();
    this.router.navigateByUrl('/recompensas');
  }

  async presentarAlerta(){
    const alert = await this.alertController.create({
      header: 'Mascota agregada',
      message: 'Ha agregado una mascota exitosamente',
      buttons: [{
        text: 'Ok',
        handler: ()=> {
          this.navigateMascotasPerdidas();
        }
      }]
    })

    alert.present();
  }
}


