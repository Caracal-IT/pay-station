import { Component, Prop, h } from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-input',
  styleUrl: 'apx-input.scss',
  shadow: true,
})
export class ApexInput {
  @Prop() caption: string;
  @Prop() ctx: Context;
  @Prop() type: string;

  @Prop({attribute: "id"}) name: string;
  @Prop({mutable: true, reflect: true}) value: string;

  private onInput(ev) {
    this.value = ev.target.value;
  }

  render() {
    return [
      <label htmlFor={this.name}>{this.caption}</label>,
      <input type={this.type} id={this.name} value={this.value} onInput={this.onInput.bind(this)}/>
    ];
  }
}
