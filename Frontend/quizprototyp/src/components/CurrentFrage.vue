<template>
   <v-card>
      <v-card-title>Aktuelle Frage</v-card-title>
      <v-card-subtitle>{{ currentFrage }}</v-card-subtitle>
      <v-card-text>
         <img :src="qrCodeUrl">
      </v-card-text>
      <v-card-actions>
         <v-btn v-on:click="LoadNextFrage" color="#383732">Nächste Frage</v-btn>
      </v-card-actions>
   </v-card>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class CurrentFrage extends Vue {
  @Prop() private guid: string = '';

  private currentFrage: string = '';
  private qrValue: string = 'TestQRCode';

  private async LoadNextFrage() {
      const response = await axios.get('http://' + window.location.hostname + ':8888/api/frage?guid=' + this.guid);
      this.currentFrage = response.data.frageText;
      this.qrValue = response.data.id;
  }

  @Watch('guid')
  private onPropertyChanged(value: string, oldValue: string) {
    this.LoadNextFrage();
  }

  private get qrCodeUrl(): string {
      return `https://api.qrserver.com/v1/create-qr-code/?data=${this.qrValue}&size=150x150`;
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
