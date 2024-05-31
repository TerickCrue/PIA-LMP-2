import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from 'src/app/interface/login-request';
import { LoginResponse } from 'src/app/interface/login-response';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private dataUrl = 'login/'
  constructor(private http: HttpClient) { }

  public authenticate(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.dataUrl + 'authenticate', loginRequest);
  }
}
