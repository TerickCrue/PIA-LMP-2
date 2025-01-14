import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { RecompensasPageRoutingModule } from './recompensas-routing.module';

import { RecompensasPage } from './recompensas.page';

//import { MascotaPerdidaService } from '../services/mascota-perdida.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RecompensasPageRoutingModule
  ],
  declarations: [RecompensasPage]
})
export class RecompensasPageModule {}
