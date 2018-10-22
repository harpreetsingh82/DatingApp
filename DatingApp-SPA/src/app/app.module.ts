import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule } from '@angular/common/http';
import {FormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent  } from './value/value.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ServerComponent } from './server/server.component';
import { ServersComponent } from './servers/servers.component';


@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavbarComponent,
      ServerComponent,
      ServersComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
