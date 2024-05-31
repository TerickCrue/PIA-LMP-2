import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { IonicStorageModule } from '@ionic/storage-angular';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './auth/token.interceptor';
//import {}

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, HttpClientModule ,IonicModule.forRoot(), AppRoutingModule, IonicModule.forRoot(),
    IonicStorageModule.forRoot()],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }, 
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    HttpClientModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
