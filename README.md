# Asset Ops

[![License: Source-Available (Apache 2.0 + Commons Clause)](https://img.shields.io/badge/License-Apache_2.0_w%2F_Commons_Clause-orange.svg)](LICENSE)
[![Platform: Windows](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)]()

A small-medium size application I made to act as a trading journal software allwing traders to keep track of their operation on the market and their performance over time locally without relaying on paid subscription services'

---

## Screenshots
![Application Screenshot](docs/images/Screenshot(21).png)
![Application Screenshot](docs/images/Screenshot(22).png)
![Application Screenshot](docs/images/Screenshot(23).png)
![Application Screenshot](docs/images/Screenshot(24).png)
![Application Screenshot](docs/images/Screenshot(25).png)
![Application Screenshot](docs/images/Screenshot(26).png)
![Application Screenshot](docs/images/Screenshot(27).png)
![Application Screenshot](docs/images/Screenshot(28).png)
![Application Screenshot](docs/images/Screenshot(29).png)
![Application Screenshot](docs/images/Screenshot(30).png)
![Application Screenshot](docs/images/Screenshot(31).png)
![Application Screenshot](docs/images/Screenshot(32).png)
---

## Features

* **Trading Record:** Allows to register trading market operations, create and edit records with trading data such as asset type, symbol name, volume, open/close price, date, any commissions, leverage and even add notes, images or mistakes you think you could have made. Filter trades by most of the afore mentioned variables.

* **Trading Portfolio:** Allows to create a trading portfolio including any type of asset pair (with their correponding type, e.g Forex, crypto etc) that would be    later used to register trading operations.

* **Risk Management Plans:** Allows to create customized risk management plans by setting name, intermediary (the platform you would use to operate, e.g BingX) max risk your willing to assume on a daily basis or per operation, daily goal and a risk-reward ration, also you can later add any relevant notes you deeme needed. Warnings displayed on operations (trades) that violate risk management plan directives such as daily goal and maximun daily loss as well as warnings in case daily goals are reached and overtrading is detected.

* **Performance Tracking:** Allows to track user account performance over time, with a time based chart displaying your account performance and return on investment by date with options to filter daily, monthly or yearly. A pie chart is also included as part of the risk management feature showing what percentage of your registered operarions uses a determined risk management plan in order to keep track of what works for you the best.

* **Export and Import Data:** Allows to export and import sqlite database.db files for persistance.

* **Built In Calculator:** A lightweight return on investment and target price calulator is included, very similar to the ones present in some trading platforms it allows you to before hand calculate the return on investment you would get by providing input for the amount to invest, the open/close price of the asser in question, the type of operation to perform (short/long) and the leverage that would be used. The same can be done the other way around with target price by providing the desired return of investment instead of close price to determine what price would the asset need to reach in order to reach that ROI.

* **Themes:** Includes both light and dark themes.

* **Localization Support:** Supports 9 languages: English, Spanish, chinese, Japanese, Russian, German, Italian, French and Portuguese.

* **WIP:** This is a work in progress project I started back when I was surfing the trading world and part of my learning curve. I have not been continously working on it, localization is not complete, some features might not behave or be as expected by an expert trader and there might be bugs, any feedback or suggestions is appreciated.

* **Source Available:** This an source available project licensed under Apache 2.0 + Commons Clause - see the LICENSE file for details.

---

## Third Party Notices

* This application uses open-source components and third-party libraries. A complete list of dependencies, copyrights, and their respective licenses can be found in THIRD-PARTY-NOTICES.txt.

---

## Download & Installation

1. Go to the [Releases](https://github.com/ADRIANTEJA/AssetOps/releases) section.
2. Download the latest installer or zipped portable release.
3. Extract or run the installer to launch the application.

---

## Building from Source

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
* Visual Studio 2022 / VS Code / Rider

### Build Instructions
1. Clone the repository:
   ```bash
   git clone [https://github.com/ADRIANTEJA/AssetOps.git](https://github.com/ADRIANTEJA/AssetOps.git)
   cd your-repo-name