import { Component, TemplateRef } from '@angular/core';
import { faEllipsisH, faPlus } from '@fortawesome/free-solid-svg-icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { CreateTodoListCommand, CreateWalletCommand, GetAllWalletVm, IWalletClient, TodoItemDto,  TodoListDto, TodoListsClient, TodosVm, TransactionClient,  UpdateTodoItemDetailCommand, UpdateTodoListCommand, UpdateWalletCommand, WalletClient } from '../web-api-client';

@Component({
  selector: 'app-home',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent {
  model: CreateWalletCommand = new CreateWalletCommand();
  debug = false;

  vm: GetAllWalletVm[];

  selectedList: GetAllWalletVm[];
  selectedItem: GetAllWalletVm;

  newListEditor: any = {};
  listOptionsEditor: any = {};
  itemDetailsEditor: any = {};

  newListModalRef: BsModalRef;
  listOptionsModalRef: BsModalRef;
  deleteListModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;

  constructor(private listsClient: TodoListsClient, private walletService: WalletClient, private itemsClient: TransactionClient, private modalService: BsModalService) {
    this.getWallets();
  }

  //get list 
  getWallets() {
    this.walletService.getList().subscribe(
      result => {
        this.vm = result;
        if (this.vm.length) {
          this.selectedList = this.vm;
        }
      },
      error => console.error(error)
    );
  }
  // Lists
  remainingItems(list: TodoListDto): number {
    return list.items.filter(t => !t.done).length;
  }

  showNewListModal(template: TemplateRef<any>): void {
    this.newListModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById("title").focus(), 250);
  }

  newListCancelled(): void {
    this.newListModalRef.hide();
    this.newListEditor = {};
  }

  addList(): void {
    let list = TodoListDto.fromJS({
      id: 0,
      title: this.newListEditor.title,
      items: [],
    });

    this.listsClient.create(<CreateTodoListCommand>{ title: this.newListEditor.title });
  }

  showListOptionsModal(template: TemplateRef<any>) {
    // this.listOptionsEditor = {
    //   id: this.selectedList.id,
    //   title: this.selectedList.title,
    // };

    this.listOptionsModalRef = this.modalService.show(template);
  }

  // updateListOptions() {
  //   this.listsClient.update(this.selectedList.id, UpdateTodoListCommand.fromJS(this.listOptionsEditor))
  //     .subscribe(
  //       () => {
  //         this.selectedList.title = this.listOptionsEditor.title,
  //           this.listOptionsModalRef.hide();
  //         this.listOptionsEditor = {};
  //       },
  //       error => console.error(error)
  //     );
  // }

  confirmDeleteList(template: TemplateRef<any>) {
    this.listOptionsModalRef.hide();
    this.deleteListModalRef = this.modalService.show(template);
  }

  // deleteListConfirmed(): void {
  //   this.listsClient.delete(this.selectedList.id).subscribe(
  //     () => {
  //       this.deleteListModalRef.hide();
  //       this.vm.lists = this.vm.lists.filter(t => t.id != this.selectedList.id)
  //       this.selectedList = this.vm.lists.length ? this.vm.lists[0] : null;
  //     },
  //     error => console.error(error)
  //   );
  // }

  // Items

  showItemDetailsModal(template: TemplateRef<any>, item: GetAllWalletVm): void {
    this.selectedItem = item;
    this.itemDetailsEditor = {
      ...this.selectedItem
    };

    this.itemDetailsModalRef = this.modalService.show(template);
  }

  // updateItemDetails(): void {
  //   this.itemsClient.updateItemDetails(this.selectedItem.id, UpdateTodoItemDetailCommand.fromJS(this.itemDetailsEditor))
  //     .subscribe(
  //       () => {
  //         if (this.selectedItem.listId != this.itemDetailsEditor.listId) {
  //           this.selectedList.items = this.selectedList.items.filter(i => i.id != this.selectedItem.id)
  //           let listIndex = this.vm.lists.findIndex(l => l.id == this.itemDetailsEditor.listId);
  //           this.selectedItem.listId = this.itemDetailsEditor.listId;
  //           this.vm.lists[listIndex].items.push(this.selectedItem);
  //         }

  //         this.selectedItem.priority = this.itemDetailsEditor.priority;
  //         this.selectedItem.note = this.itemDetailsEditor.note;
  //         this.itemDetailsModalRef.hide();
  //         this.itemDetailsEditor = {};
  //       },
  //       error => console.error(error)
  //     );
  // }


  editItem(item: GetAllWalletVm, inputId: string): void {
    this.selectedItem = item;
    setTimeout(() => document.getElementById(inputId).focus(), 100);
  }


  updateIsActiveItem(event,item: GetAllWalletVm): void {
    debugger
    this.UnSelectAll();
    this.walletService.update(item.id, UpdateWalletCommand.fromJS(item))
      .subscribe(
        () => console.log('Update succeeded.'),
        error => console.error(error)
      );
    event.target.checked = item.iscActive;
    this.selectedItem = null;
  }
 UnSelectAll() {
    var items = document.getElementsByName('flexRadioDefault') as any;
    for (var i = 0; i < items.length; i++) {
        if (items[i].type == 'checkbox')
            items[i].checked = false;
    }
}
  updateItem(item: GetAllWalletVm, pressedEnter: boolean = false): void {
    debugger
    let isNewItem = item.id == undefined;

    if (!item.title.trim()) {
      this.deleteItem(item);
      return;
    }

    if (item.id == undefined) {
      //you can create item
    } else {
      this.walletService.update(item.id, UpdateWalletCommand.fromJS(item))
        .subscribe(
          () => console.log('Update succeeded.'),
          error => console.error(error)
        );
    }

    this.selectedItem = null;

    if (isNewItem && pressedEnter) {
      // this.addItem();
    }
  }

  // Delete item
  deleteItem(item: GetAllWalletVm) {
    if (this.itemDetailsModalRef) {
      this.itemDetailsModalRef.hide();
    }

    if (item.id == undefined) {
      let itemIndex = this.selectedList.indexOf(this.selectedItem);
      this.selectedList.splice(itemIndex, 1);
    } else {
      this.walletService.delete(item.id).subscribe(
        () => this.selectedList = this.selectedList.filter(t => t.id != item.id),
        error => console.error(error)
      );
    }
  }

  createWallet() {
    debugger
    this.walletService.create(this.model).subscribe(
      result => {
        this.newListModalRef.hide();
        this.newListEditor = {};
        this.getWallets();
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.newListEditor.error = errors.Title[0];
        }

        setTimeout(() => document.getElementById("title").focus(), 250);
      }
    );
  }
}
