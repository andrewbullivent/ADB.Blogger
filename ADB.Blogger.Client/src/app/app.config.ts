import { provideRouter, withComponentInputBinding } from "@angular/router";
import { APP_ROUTES } from "./app.routes";
import { ApplicationConfig } from "@angular/core";
import { provideHttpClient } from '@angular/common/http';


export const appConfig: ApplicationConfig = {
    providers: [provideRouter(APP_ROUTES, withComponentInputBinding()), provideHttpClient()]
  };