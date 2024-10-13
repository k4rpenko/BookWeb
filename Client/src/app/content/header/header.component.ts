import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SlideMenuComponent } from '../../moduls/slide-menu/slide-menu.component';
import { AuthComponent } from '../../moduls/auth/auth.component';
import { SlideShopingCardComponent } from '../../moduls/ShopingCard/slide-shoping-card/slide-shoping-card.component';
import { CartService } from '../../data/card/cart.service';
import { AccountContent, Content } from '../../data/interface/Account.interface';
import { GlobalState } from '../../global-types';
import { CookieService } from 'ngx-cookie-service';
import { CommonModule } from '@angular/common';
import { AccountContents } from '../../data/GET/AccountContent/AccountContents.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [  CommonModule ],
  providers: [CookieService],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit{
  length: number = 0;
  profileServiceAccount = inject(AccountContents);
  accountContent!: AccountContent;
  
  constructor(public dialog: MatDialog, private cartService: CartService, private cookieService: CookieService) {
    this.profileServiceAccount.GetAccountContent(this.cookieService.get('UserId')).subscribe(response  => {
      this.accountContent = response; 
      console.log(this.accountContent.account);
    });

  }

  ValidAccount: boolean = false;
  UrlAvatar: string = ""

  ngOnInit() {
    this.cartService.cart$.subscribe(() => {
      this.length = this.cartService.getTotalBook();
    });

    this.ValidAccount = GlobalState.ValidAccount; 
  }

  openDialog(): void {
    this.dialog.open(SlideMenuComponent, {
      width: '410px',
      height: '100%',
      position: { left: '0', top: '0' },
      panelClass: 'custom-container'
    });
  }

  openAuth(): void {
    this.dialog.open(AuthComponent, {});
  }

  openShopCard(): void{
    this.dialog.open(SlideShopingCardComponent, {
      width: '410px',
      height: '100%',
      position: { right: '0', top: '0' },
      panelClass: 'custom-container'
    });
  }
}
