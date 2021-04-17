import { Component, Event, EventEmitter, Prop, h } from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-checkbox',
  styleUrl: 'apx-checkbox.scss',
  shadow: true,
})
export class ApexCheckBox {
  @Prop() caption: string;
  @Prop() ctx: Context;

  @Prop({attribute: "id"}) name: string;
  @Prop({mutable: true, reflect: true}) value: boolean;

  @Event() checkChanged: EventEmitter<boolean>;

  private onInput(ev) {
    this.value = ev.target.checked;
    this.checkChanged.emit(this.value);
  }

  render() {
    return [
      <input type='checkbox' id={this.name} checked={this.value} onInput={this.onInput.bind(this)}/>,
      <label htmlFor={this.name}/>,
      <label htmlFor={this.name}>{this.caption}</label>
    ];
  }
}
