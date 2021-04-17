import { Component, Prop, h } from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-grid',
  styleUrl: 'apx-grid.scss',
  shadow: true,
})
export class ApexGrid {
  @Prop() headers: any;
  @Prop() caption: string;
  @Prop() ctx: Context;
  @Prop({mutable: true, reflect: true}) value: Array<any>;

  onStatusInput(evt) {
    const updateStatus = [
      { "withdrawalId": 2, "status": evt.target.value }
    ]

    this.ctx.model.setValue("withdrawals.status", updateStatus);
  }

  onSelected(item, evt) {
    item.selected = evt.detail;
    this.value = [...this.value];
  }

  onSelectedRow(item) {
    item.selected = !item.selected||false;
    this.value = [...this.value];
  }

  componentWillRender() {
    if(this.headers || !this.value || this.value.length < 1) return;

    this.headers = Object.keys(this.value[0])
                         .map(this.createHeader);
  }

  createHeader(header) {
    return {
      key: header,
      text: (header).replace(/([A-Z])/g, ' $1').trim()
    };
  }

  renderHeader() {
    return <tr>
      <th/>
      {this.headers.map(head => <th>{head.text}<resize-handle/></th>)}
    </tr>;
  }

  renderRow(i) {
    return <tr onClick={this.onSelectedRow.bind(this, i)}  class={i.selected?'selected':''}>
      <td><apx-checkbox value={i.selected} onCheckChanged={this.onSelected.bind(this, i)}/></td>
      {this.headers.map(head => <td>{i[head.key]}<resize-handle/></td>)}
    </tr>;
  }

  render() {
    if(!this.value)
      return null;

    return [
      <div><h2>{this.caption}</h2></div>,
      <div><span>Status for 2</span><input onInput={this.onStatusInput.bind(this)} /></div>,
      <table>
        <thead>{this.renderHeader()}</thead>
        <tbody>{this.value.map(this.renderRow.bind(this))}</tbody>
      </table>
    ];
  }
}
