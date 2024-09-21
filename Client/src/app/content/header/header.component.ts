import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SlideMenuComponent } from '../../moduls/slide-menu/slide-menu.component';
import { AuthComponent } from '../../moduls/auth/auth.component';
import { SlideShopingCardComponent } from '../../moduls/ShopingCard/slide-shoping-card/slide-shoping-card.component';
import { CartService } from '../../data/card/cart.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  length: number = 0;

  constructor(public dialog: MatDialog, private cartService: CartService) {}

  ngOnInit() {
    this.cartService.cart$.subscribe(() => {
      this.length = this.cartService.getTotalBook();
      console.log(this.length = this.cartService.getTotalBook());
    });
  }

  openDialog(): void {
    this.dialog.open(SlideMenuComponent, {
      width: '300px',
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
      width: '300px',
      height: '100%',
      position: { right: '0', top: '0' },
      panelClass: 'custom-container'
    });
  }
}
