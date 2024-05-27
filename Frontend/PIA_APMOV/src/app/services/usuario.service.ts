import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private dataUrl = environment.urlBackend + 'usuario/'

  constructor(public http: HttpClient) {

  }

  //public consultarUsuarioPorId

}
