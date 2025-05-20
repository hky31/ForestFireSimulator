// import { bootstrapApplication } from '@angular/platform-browser';
// import { appConfig } from './app/app.config';
// import { AppComponent } from './app/app.component';
// import { provideHttpClient, HttpClientModule } from '@angular/common/http';
// import { importProvidersFrom } from '@angular/core';

// bootstrapApplication(AppComponent, appConfig).catch((err) =>
//   console.error(err)
// );

// bootstrapApplication(AppComponent, {
//   providers: [provideHttpClient()], // ✅ C'est ça qui remplace HttpClientModule
// });
// bootstrapApplication(AppComponent, {
//   providers: [importProvidersFrom(HttpClientModule)],
// });

import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient()],
}).catch((err) => console.error(err));
