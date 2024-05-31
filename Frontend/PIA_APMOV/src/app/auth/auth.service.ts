import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from './../services/utils/storage.service';
//import { GeneralConstant } from '@utils/general-constant';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private storage: StorageService,
    private router: Router
  ) {}

  public async obtenerToken(): Promise<string | null> {
    return await this.storage.read('userToken');
  }

  public async guardarToken(token: string): Promise<void> {
    await this.storage.set('userToken', token);
  }

  public async cerrarSesion(reroute: boolean = true): Promise<void> {
    await this.storage.remove('userToken');

    if (reroute) {
      this.router.navigate(['/login']);
    }
  }

  public async isAuthenticated(): Promise<boolean> {
    const token = await this.obtenerToken();

    return token !== null && token !== undefined;
  }
}
