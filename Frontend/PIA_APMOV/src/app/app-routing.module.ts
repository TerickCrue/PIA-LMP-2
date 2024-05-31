import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth-guard.service';
import { LoginPage } from './forms/login/login.page';
import { RegisterPage } from './forms/register/register.page';

const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadChildren: () => import('./forms/login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'register',
    loadChildren: () => import('./forms/register/register.module').then( m => m.RegisterPageModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'account',
    loadChildren: () => import('./account/account.module').then( m => m.AccountPageModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'recompensas',
    loadChildren: () => import('./recompensas/recompensas.module').then( m => m.RecompensasPageModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'map-aviso',
    loadChildren: () => import('./map-aviso/map-aviso.module').then( m => m.MapAvisoPageModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'radar-mascotas',
    loadChildren: () => import('./radar-mascotas/radar-mascotas.module').then( m => m.RadarMascotasPageModule)
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
