import { JWTInterceptor } from './interceptors/jwt-interceptor';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  HTTP_INTERCEPTORS,
  HttpClientModule,
  HttpClient,
} from '@angular/common/http';

import { VgCoreModule } from '@videogular/ngx-videogular/core';
import { VgControlsModule } from '@videogular/ngx-videogular/controls';
import { VgOverlayPlayModule } from '@videogular/ngx-videogular/overlay-play';
import { VgBufferingModule } from '@videogular/ngx-videogular/buffering';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { NavComponent } from './components/nav/nav.component';
import { FeedComponent } from './components/feed/feed.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HomeComponent } from './components/home/home.component';
import { UserFollowCardComponent } from './components/widgets/user-follow-card/user-follow-card.component';
import { PostCardComponent } from './components/widgets/post-card/post-card.component';
import { AddPostComponent } from './components/widgets/add-post/add-post.component';
import { AddPostDialogComponent } from './components/widgets/add-post-dialog/add-post-dialog.component';
import { SuggestedFriendsComponent } from './components/widgets/suggested-friends/suggested-friends.component';
import { UserFriendsComponent } from './components/widgets/user-friends/user-friends.component';
import { SignupComponent } from './components/signup/signup.component';
import { ReactOptionsComponent } from './components/widgets/react-options/react-options.component';
import { PostCommentsComponent } from './components/widgets/post-comments/post-comments.component';
import { CommentComponent } from './components/widgets/comment/comment.component';

import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { MenuModule } from 'primeng/menu';
import { TabViewModule } from 'primeng/tabview';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { PanelModule } from 'primeng/panel';
import { FileUploadModule } from 'primeng/fileupload';
import { EditorModule } from 'primeng/editor';
import { ToastModule } from 'primeng/toast';
import { SplitButtonModule } from 'primeng/splitbutton';
import { DialogModule } from 'primeng/dialog';
import { DataViewModule } from 'primeng/dataview';
import { DropdownModule } from 'primeng/dropdown';
import { AccordionModule } from 'primeng/accordion';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { DockModule } from 'primeng/dock';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { InplaceModule } from 'primeng/inplace';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { PendingRequestsComponent } from './components/widgets/pending-requests/pending-requests.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { PrivacySettingsComponent } from './components/privacy-settings/privacy-settings.component';
import { PrettyDatePipe } from './pipes/pretty-date';
import { MediaUrlPipe } from './pipes/media-url';
import { NgApexchartsModule } from 'ng-apexcharts';
import { BigMoversComponent } from './components/widgets/big-movers/big-movers.component';

@NgModule({
  declarations: [
    PrettyDatePipe,
    MediaUrlPipe,

    AppComponent,
    UserProfileComponent,
    NavComponent,
    FeedComponent,
    LoginComponent,
    PageNotFoundComponent,
    HomeComponent,
    PostCardComponent,
    UserFollowCardComponent,
    AddPostComponent,
    AddPostDialogComponent,
    SuggestedFriendsComponent,
    UserFriendsComponent,
    SignupComponent,
    ReactOptionsComponent,
    PostCommentsComponent,
    CommentComponent,
    PendingRequestsComponent,
    SideBarComponent,
    PrivacySettingsComponent,
    BigMoversComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgApexchartsModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule,
    CheckboxModule,
    InputTextModule,
    ButtonModule,
    CardModule,
    AutoCompleteModule,
    MenuModule,
    TabViewModule,
    AvatarModule,
    AvatarGroupModule,
    InputTextareaModule,
    PanelModule,
    FileUploadModule,
    EditorModule,
    ToastModule,
    SplitButtonModule,
    DialogModule,
    DataViewModule,
    DropdownModule,
    AccordionModule,
    ToggleButtonModule,
    SelectButtonModule,
    DockModule,
    OverlayPanelModule,
    ProgressSpinnerModule,
    InplaceModule,
    ConfirmDialogModule,
    ConfirmPopupModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JWTInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
