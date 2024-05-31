import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Usuario } from 'src/app/interface/usuario';
import { Observable } from 'rxjs';
import { UsuarioDto } from 'src/app/interface/usuario-dto';
import { NuevoUsuarioDto } from '../../interface/nuevo-usuario-dto';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private dataUrl = 'usuario/'

  constructor(public http: HttpClient) {

  }

  public consultarUsuarioSesion(): Observable<UsuarioDto>{
    return this.http.get<UsuarioDto>(this.dataUrl + 'consultarUsuario')
  }

  public actualizarInformacionUsuario(usuarioDto: UsuarioDto): Observable<UsuarioDto>{
    return this.http.put<UsuarioDto>(this.dataUrl + 'actualizar', usuarioDto);
  }

  public agregarNuevoUsuario(usuarioDto: NuevoUsuarioDto): Observable<void>{
    return this.http.post<void>(this.dataUrl + 'agregar', usuarioDto);
  }


}
