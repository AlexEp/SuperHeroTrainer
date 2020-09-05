import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

//Plugins
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


//App Components
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { AdminComponent } from './components/admin/admin.component';
import { EditTrainerComponent } from './components/admin/edit-trainer/edit-trainer.component';
import { RegistrationTrainerComponent } from './components/admin/registration-trainer/registration-trainer.component';
import { LoginComponent } from './components/login/login.component';


//App Services
import { AlertService } from './shared/services/alert.service';
import { WelcomeScreenComponent } from './components/welcome-screen/welcome-screen.component';
import { HeroService } from './_services/heroes.service';
import { AuthService } from './shared/services/auth.service';

//App else
import { OrderByPowerPipe } from './shared/pipes/order-by-power.pipe';
import { AuthInterceptor } from './shared/services/auth.interceptor';
import { appRoutes } from './routes';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    NavBarComponent,
    AdminComponent,
    EditTrainerComponent,
    RegistrationTrainerComponent,
    WelcomeScreenComponent,
    OrderByPowerPipe,

  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes),
    NgbModule,
  ],
  providers: [
    AuthService,
    AlertService,
    HeroService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
