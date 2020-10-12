import { ImageService } from './services/image.service';
import { SearchService } from './services/search.service';
import { Helpers } from './helpers/helpers';
import { RouterModule } from '@angular/router';
import { AppComponent } from './layout/app/app.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FooterComponent } from './layout/footer/footer.component';
import { HeadComponent } from './layout/head/head.component';
import { LoginComponent } from './components/login/login.component';
import { FeedComponent } from './components/feed/feed.component';
import { ProfileComponent } from './components/profile/profile.component';
import { PhotocardComponent } from './shared/photocard/photocard.component';
import { SearchComponent } from './shared/search/search.component';
import { ImageUploadComponent } from './components/image-upload/image-upload.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UsersComponent } from './components/users/users.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastsContainerComponent } from './shared/toasts-container/toasts-container.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeadComponent,
    LoginComponent,
    FeedComponent,
    ProfileComponent,
    PhotocardComponent,
    SearchComponent,
    ImageUploadComponent,
    UsersComponent,
    ToastsContainerComponent,
  ],
  imports: [
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserModule,
    NgbModule,
    Ng2SearchPipeModule,
  ],
  providers: [Helpers, SearchService, ImageService],
  bootstrap: [AppComponent],
})
export class AppModule {}
