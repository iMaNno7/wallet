import { Component } from '@angular/core';
import { WalletClient } from '../web-api-client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  /**
   *
   */
  vm :any;
  constructor(private walletService: WalletClient) {
    this.walletService.get().subscribe(
      result => {
        this.vm = result;        
      },
      error => console.error(error)
    );
  }
}
