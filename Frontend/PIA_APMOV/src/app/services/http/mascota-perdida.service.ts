import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MascotaPerdida } from '../../interface/mascota-perdida';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MascotaPerdidaService {

  private dataUrl = 'mascotaPerdida/'

  constructor(public http: HttpClient) {}

  public consultarMascotas(): Observable<MascotaPerdida[]>{
    return this.http.get<MascotaPerdida[]>(this.dataUrl + 'consultarMascotas')
  }

  public agregarMascota(mascota: MascotaPerdida): Observable<MascotaPerdida>{
    return this.http.post<MascotaPerdida>(this.dataUrl + 'agregar', mascota )
  }
}
